using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Domain.Fields;
using IMMRequest.Logic.Exceptions;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;
using IMMRequest.Logic.Tests;
using System;
using System.Linq;

namespace IMMRequest.Logic.Core
{
    using Type = Domain.Type;

    public class RequestsLogic : IRequestsLogic
    {
        private readonly IRepository<Request> _requestRepo;
        private readonly IRepository<Domain.Type> _typeRepo;
        private readonly IAreaQueries _areaQueries;

        public RequestsLogic(
               IRepository<Request> requestRepository,
               IRepository<Domain.Type> typeRepository,
               IAreaQueries areaQueries
            )
        {
            this._requestRepo = requestRepository;
            this._typeRepo = typeRepository;
            this._areaQueries = areaQueries;
        }

        public int Add(CreateRequest createRequest)
        {
            var type = _typeRepo.Get(createRequest.TopicId);

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

                    default:
                        break;
                }
            }

            ValidateNoRequiredFieldsAreMissing(createRequest, type);

            var request = new Request
            {
                Citizen = new Citizen { Email = createRequest.Email, Name = createRequest.Name, PhoneNumber = createRequest.Phone },
                Details = createRequest.Details,
                Type = type,
                FieldValues = new System.Collections.Generic.List<RequestField>(),
            };

            AddRequestFieldsToRequest(createRequest, type, request);

            _requestRepo.Add(request);

            return request.Id;
        }

        private void AddRequestFieldsToRequest(CreateRequest createRequest, Type type, Request request)
        {
            var fieldsWithType = type.AdditionalFields.Join(
                createRequest.AdditionalFields,
                af => af.Name,
                crf => crf.Name,
                (af, crf) => new {Type = af.FieldType, Name = crf.Name, Value = crf.Value}
            );

            // create additional fields list
            foreach (var field in fieldsWithType)
            {
                switch (field.Type)
                {
                    case Domain.Fields.FieldType.Date:
                        request.FieldValues.Add(new DateRequestField {Name = field.Name, Value = DateTime.Parse(field.Value)});
                        break;
                    case Domain.Fields.FieldType.Integer:
                        request.FieldValues.Add(new IntRequestField {Name = field.Name, Value = int.Parse(field.Value)});
                        break;
                    case Domain.Fields.FieldType.Text:
                        request.FieldValues.Add(new TextRequestField {Name = field.Name, Value = field.Value});
                        break;
                }
            }
        }

        #region Validation Methods
        private void ValidateNoRequiredFieldsAreMissing(CreateRequest createRequest, Type type)
        {
            var requiredFields = type.AdditionalFields.Where(af => af.IsRequired).Select(af => af.Name);
            var providedFields = createRequest.AdditionalFields.Select(x => x.Name);
            var nonProvidedRequiredFields =
                requiredFields.Except<string>(providedFields, StringComparer.InvariantCultureIgnoreCase);
            var providedRequiredFields = nonProvidedRequiredFields as string[] ?? nonProvidedRequiredFields.ToArray();
            if (providedRequiredFields.Any())
            {
                throw new LessAdditionalFieldsThanRequiredException(
                    $"Required fields {string.Join(", ", providedRequiredFields.ToArray())} should have been provided");
            }
        }

        private static void ValidateStringValueNotNullOrEmpty(FieldRequest additionalField)
        {
            if (string.IsNullOrEmpty(additionalField.Value) || string.IsNullOrWhiteSpace(additionalField.Value))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value for field with name {additionalField.Name} cannot be empty or null");
            }
        }

        private int TryToParseIntValue(FieldRequest additionalField)
        {
            if (!int.TryParse(additionalField.Value, out var num))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value '{additionalField.Value}' for field with name {additionalField.Name} cannot be read as an integer");
            }

            return num;
        }

        private DateTime TryToParseDateValue(FieldRequest additionalField)
        {
            if (!DateTime.TryParse(additionalField.Value, out var parseDate))
            {
                throw new InvalidFieldValueCastForFieldTypeException(
                    $"value '{additionalField.Value}' for field with name {additionalField.Name} cannot be read as a date");
            }

            return parseDate;
        }

        private AdditionalField FindFieldTemplateWithName(Type type, FieldRequest additionalField)
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

        private void ValidateTypeNotNull(CreateRequest createRequest, Domain.Type type)
        {
            if (type == null)
            {
                throw new NoSuchTopicException($"No topic with id={createRequest.TopicId} exists");
            }
        }

        #endregion Validation Methods
        //public GetStatusRequestResponse GetRequestStatus(int requestId)
        //{
        //    ValidateRequestId(requestId);

        //    var request = this._requestRepo.Get(requestId);

        //    ValidateRequestNotNull(requestId, request);

        //    var area = this._areaQueries.FindWithTypeId()
        //    return new GetStatusRequestResponse
        //    {
        //        Details = request.Details,
        //        RequestState = request.Status.Description,
        //        CitizenName = request.Citizen.Name,
        //        CitizenPhoneNumber = request.Citizen.PhoneNumber,
        //        CitizenEmail = request.Citizen.Email
        //    };
        //}
    }
}
