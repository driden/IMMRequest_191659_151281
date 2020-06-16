namespace IMMRequest.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Fields;
    using Exceptions.AdditionalField;
    using Exceptions.Request;
    using Exceptions.Type;
    using Interfaces;
    using Models.Request;
    using Type = Domain.Type;

    public class RequestsLogic : IRequestsLogic
    {
        private readonly IRepository<Request> _requestRepository;
        private readonly IRepository<Type> _typeRepository;

        public RequestsLogic(
               IRepository<Request> requestRepository,
               IRepository<Type> typeRepository
            )
        {
            _requestRepository = requestRepository;
            _typeRepository = typeRepository;
        }

        public int Add(CreateRequest createRequest)
        {
            var type = _typeRepository.Get(createRequest.TypeId);

            ValidateTypeNotNull(createRequest, type);

            ValidateRequestFieldCount(createRequest, type);

            foreach (var additionalField in createRequest.AdditionalFields)
            {
                var correspondingField = FindFieldTemplateWithName(type, additionalField);

                switch (correspondingField.FieldType)
                {
                    case FieldType.Date:
                        var parseDate = TryToParseDateValue(additionalField);
                        correspondingField.ValidateRange(parseDate);
                        break;

                    case FieldType.Integer:
                        var num = TryToParseIntValue(additionalField);
                        correspondingField.ValidateRange(num);
                        break;

                    case FieldType.Text:
                        ValidateStringValueNotNullOrEmpty(additionalField);
                        correspondingField.ValidateRange(additionalField.Value);
                        break;
                }
            }

            ValidateNoRequiredFieldsAreMissing(createRequest, type);

            var request = new Request
            {
                Citizen = new Citizen { Email = createRequest.Email, Name = createRequest.Name, PhoneNumber = createRequest.Phone },
                Details = createRequest.Details,
                Type = type,
                FieldValues = new List<RequestField>(),
            };

            AddRequestFieldsToRequest(createRequest, type, request);

            _requestRepository.Add(request);

            return request.Id;
        }

        public RequestModel GetRequestStatus(int requestId)
        {
            ValidateRequestId(requestId);

            var request = _requestRepository.Get(requestId);

            ValidateRequestNotNull(requestId, request);

            //var area = this._areaQueries.FindWithTypeId(request.Type.Id);
            return new RequestModel
            {
                Details = request.Details,
                RequestState = request.Status.Description,
                CitizenName = request.Citizen.Name,
                CitizenPhoneNumber = request.Citizen.PhoneNumber,
                CitizenEmail = request.Citizen.Email,
                Fields = request.FieldValues.Select(fv => new FieldRequestModel { Name = fv.Name, Value = fv.ToString() }),
                RequestId = request.Id
            };
        }

        public IEnumerable<RequestStatusModel> GetAllRequests()
        {
            return _requestRepository.GetAll().Select(req => new RequestStatusModel
            {
                Details = req.Details,
                RequestId = req.Id,
                RequestedBy = req.Citizen.Email,
                Status = req.Status.Description
            });
        }

        public void UpdateRequestStatus(int requestId, string newState)
        {
            var validStates = new[] { "Created", "InReview", "Denied", "Accepted", "Done" };

            if (!validStates.Contains(newState, StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidStateNameException($"state '{newState}' is not a valid state.");
            }

            ValidateRequestId(requestId);
            var request = _requestRepository.Get(requestId);
            ValidateRequestNotNull(requestId, request);

            switch (newState.ToLower())
            {
                case "created":
                    request.Status.Created();
                    break;
                case "inreview":
                    request.Status.InReview();
                    break;
                case "denied":
                    request.Status.Denied();
                    break;
                case "accepted":
                    request.Status.Accepted();
                    break;
                case "done":
                    request.Status.Done();
                    break;
            }

            _requestRepository.Update(request);
        }

        private void AddRequestFieldsToRequest(CreateRequest createRequest, Type type, Request request)
        {
            var fieldsWithType = type.AdditionalFields.Join(
                createRequest.AdditionalFields,
                af => af.Name,
                crf => crf.Name,
                (af, crf) => new { Type = af.FieldType, crf.Name, crf.Value }
            );

            var repeatedNames = fieldsWithType
                .GroupBy(f => f.Name)
                .Where(group => group.Count() > 1)
                .Select(f => f.Key)
                .ToList();

            if (repeatedNames.Any())
            {
                throw new InvalidAdditionalFieldForTypeException($"The entered field names({string.Join(',', repeatedNames)}) are repeated.");
            }

            // create additional fields list
            foreach (var field in fieldsWithType)
            {
                switch (field.Type)
                {
                    case FieldType.Date:
                        request.FieldValues.Add(new DateRequestField { Name = field.Name, Value = DateTime.Parse(field.Value) });
                        break;
                    case FieldType.Integer:
                        request.FieldValues.Add(new IntRequestField { Name = field.Name, Value = int.Parse(field.Value) });
                        break;
                    case FieldType.Text:
                        request.FieldValues.Add(new TextRequestField { Name = field.Name, Value = field.Value });
                        break;
                }
            }
        }

        #region Validation Methods
        private void ValidateRequestId(int requestId)
        {
            if (requestId <= 0)
            {
                throw new InvalidRequestIdException($"id {requestId} is invalid.");
            }
        }

        private void ValidateRequestNotNull(int requestId, Request request)
        {
            if (request == null)
            {
                throw new NoSuchRequestException($"Request with id {requestId} could not be found.");
            }
        }

        private void ValidateNoRequiredFieldsAreMissing(CreateRequest createRequest, Type type)
        {
            var requiredFields = type.AdditionalFields.Where(af => af.IsRequired).Select(af => af.Name);
            var providedFields = createRequest.AdditionalFields.Select(x => x.Name);
            var nonProvidedRequiredFields =
                requiredFields.Except(providedFields, StringComparer.InvariantCultureIgnoreCase);
            var providedRequiredFields = nonProvidedRequiredFields as string[] ?? nonProvidedRequiredFields.ToArray();
            if (providedRequiredFields.Any())
            {
                throw new LessAdditionalFieldsThanRequiredException(
                    $"Required fields {string.Join(", ", providedRequiredFields.ToArray())} should have been provided");
            }
        }

        private static void ValidateStringValueNotNullOrEmpty(FieldRequestModel additionalField)
        {
            if (string.IsNullOrEmpty(additionalField.Value) || string.IsNullOrWhiteSpace(additionalField.Value))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value for field with name {additionalField.Name} cannot be empty or null");
            }
        }

        private int TryToParseIntValue(FieldRequestModel additionalField)
        {
            if (!int.TryParse(additionalField.Value, out var num))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value '{additionalField.Value}' for field with name {additionalField.Name} cannot be read as an integer");
            }

            return num;
        }

        private DateTime TryToParseDateValue(FieldRequestModel additionalField)
        {
            if (!DateTime.TryParse(additionalField.Value, out var parseDate))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value '{additionalField.Value}' for field with name {additionalField.Name} cannot be read as a date");
            }

            return parseDate;
        }

        private AdditionalField FindFieldTemplateWithName(Type type, FieldRequestModel additionalField)
        {
            var correspondingField = type.AdditionalFields.FirstOrDefault(field => field.Name == additionalField.Name);
            if (correspondingField == null)
            {
                throw new NoSuchAdditionalFieldException(
                    $"There's no field named {additionalField.Name} for type with id {type.Id})");
            }

            return correspondingField;
        }

        private void ValidateRequestFieldCount(CreateRequest createRequest, Type type)
        {
            if (createRequest.AdditionalFields.Count() > type.AdditionalFields.Count)
            {
                throw new InvalidAdditionalFieldForTypeException(
                    $"Too many additional fields were sent for type with id {type.Id}");
            }
        }

        private void ValidateTypeNotNull(CreateRequest createRequest, Type type)
        {
            if (type == null)
            {
                throw new NoSuchTypeException($"No type with id={createRequest.TypeId} exists");
            }
        }

        #endregion Validation Methods

    }
}
