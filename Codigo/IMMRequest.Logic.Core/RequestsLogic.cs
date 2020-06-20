namespace IMMRequest.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Domain.Fields;
    using Exceptions.AdditionalField;
    using Exceptions.Request;
    using Exceptions.Type;
    using Interfaces;
    using Models.Request;
    using Type = Domain.Type;

    public class RequestsLogic : IRequestsLogic
    {
        private const char VALUE_SEPARATOR = '|';
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

        public int Add(CreateRequestModel createRequestModel)
        {
            var type = _typeRepository.Get(createRequestModel.TypeId);

            ValidateTypeNotNull(createRequestModel, type);

            ValidateRequestFieldCount(createRequestModel, type);

            foreach (var additionalField in createRequestModel.AdditionalFields)
            {
                var correspondingField = FindFieldTemplateWithName(type, additionalField);

                switch (correspondingField.FieldType)
                {
                    case FieldType.Date:
                        var parseDate = TryToParseDateValues(additionalField);
                        correspondingField.ValidateRange(parseDate);
                        break;

                    case FieldType.Integer:
                        var num = TryToParseIntValues(additionalField);
                        correspondingField.ValidateRange(num);
                        break;

                    case FieldType.Text:
                        ValidateStringValueNotNullOrEmpty(additionalField);
                        correspondingField.ValidateRange(additionalField.Values.Split(VALUE_SEPARATOR));
                        break;
                    case FieldType.Boolean:
                        ValidateStringCanBeABool(additionalField);
                        correspondingField.ValidateRange(additionalField.Values);
                        break;
                }
            }

            ValidateNoRequiredFieldsAreMissing(createRequestModel, type);

            var request = new Request
            {
                Citizen = new Citizen { Email = createRequestModel.Email, Name = createRequestModel.Name, PhoneNumber = createRequestModel.Phone },
                Details = createRequestModel.Details,
                Type = type,
                FieldValues = new List<RequestField>(),
            };

            AddRequestFieldsToRequest(createRequestModel, type, request);

            _requestRepository.Add(request);

            return request.Id;
        }

        public RequestModel GetRequestStatus(int requestId)
        {
            ValidateRequestId(requestId);

            var request = _requestRepository.Get(requestId);

            ValidateRequestNotNull(requestId, request);

            return new RequestModel
            {
                Details = request.Details,
                RequestState = request.Status.Description,
                CitizenName = request.Citizen.Name,
                CitizenPhoneNumber = request.Citizen.PhoneNumber,
                CitizenEmail = request.Citizen.Email,
                Fields = request.FieldValues.Select(fv =>
                    new FieldRequestModel
                    {
                        Name = fv.Name,
                        Values = fv.ToString()
                    }),
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

        private void AddRequestFieldsToRequest(CreateRequestModel createRequestModel, Type type, Request request)
        {
            var fieldsWithType = type.AdditionalFields.Join(
                createRequestModel.AdditionalFields,
                af => af.Name,
                crf => crf.Name,
                (af, crf) => new { Type = af.FieldType, crf.Name, crf.Values }
            ).ToList();

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
                var fieldValues = field.Values.Split(VALUE_SEPARATOR);
                switch (field.Type)
                {
                    case FieldType.Date:
                        request.FieldValues.Add(new DateRequestField { Name = field.Name, Values = fieldValues.Select(DateTime.Parse).ToList() });
                        break;
                    case FieldType.Integer:
                        request.FieldValues.Add(new IntRequestField { Name = field.Name, Values = fieldValues.Select(int.Parse).ToList() });
                        break;
                    case FieldType.Text:
                        request.FieldValues.Add(new TextRequestField { Name = field.Name, Values = fieldValues.ToList() });
                        break;
                    case FieldType.Boolean:
                        request.FieldValues.Add(new BooleanRequestField { Name = field.Name, Values = fieldValues.Select(bool.Parse).ToList() });
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

        private void ValidateNoRequiredFieldsAreMissing(CreateRequestModel createRequestModel, Type type)
        {
            var requiredFields = type.AdditionalFields.Where(af => af.IsRequired).Select(af => af.Name);
            var providedFields = createRequestModel.AdditionalFields.Select(x => x.Name); var nonProvidedRequiredFields =
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
            if (additionalField.Values.Split(VALUE_SEPARATOR).Any(string.IsNullOrEmpty) || additionalField.Values.Split(',').Any(string.IsNullOrWhiteSpace))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value for field with name {additionalField.Name} cannot be empty or null");
            }
        }
        private static void ValidateStringCanBeABool(FieldRequestModel additionalField)
        {
            ValidateStringValueNotNullOrEmpty(additionalField);
            var boolStrings = new[] { "true", "false" };
            var values = additionalField.Values.Split(VALUE_SEPARATOR);
            if (values.Count() > 1 || !boolStrings.Contains(values.FirstOrDefault()))
            {
                throw new InvalidFieldRangeException("a boolean field can only be 'true' or 'false'");
            }
        }

        private List<int> TryToParseIntValues(FieldRequestModel additionalField)
        {
            return additionalField.Values.Split(VALUE_SEPARATOR).Select(val =>
            {
                if (!int.TryParse(val, out var num))
                {
                    throw new InvalidFieldValueCastForFieldTypeException(
                        $"value '{val}' for field with name {additionalField.Name} cannot be read as an integer");
                }

                return num;
            }).ToList();
        }

        private List<DateTime> TryToParseDateValues(FieldRequestModel additionalField)
        {
            return additionalField.Values.Split(VALUE_SEPARATOR).Select(val =>
            {
                if (!DateTime.TryParse(val, out var parseDate))
                {
                    throw new InvalidFieldValueCastForFieldTypeException(
                        $"value '{val}' for field with name {additionalField.Name} cannot be read as a date");
                }

                return parseDate;
            }).ToList();
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

        private void ValidateRequestFieldCount(CreateRequestModel createRequestModel, Type type)
        {
            if (createRequestModel.AdditionalFields.Count() > type.AdditionalFields.Count)
            {
                throw new InvalidAdditionalFieldForTypeException(
                    $"Too many additional fields were sent for type with id {type.Id}");
            }
        }

        private void ValidateTypeNotNull(CreateRequestModel createRequestModel, Type type)
        {
            if (type == null)
            {
                throw new NoSuchTypeException($"No type with id={createRequestModel.TypeId} exists");
            }
        }

        #endregion Validation Methods

    }
}
