using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Logic.Exceptions;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;
using IMMRequest.Logic.Tests;
using System;
using System.Linq;

namespace IMMRequest.Logic.Core
{
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

            if (createRequest.AdditionalFields.Count() > type.AdditionalFields.Count)
            {
                throw new InvalidAdditionalFieldForTypeException($"Too many additional fields were sent for type with id {type.Id}");
            }

            foreach (var additionalField in createRequest.AdditionalFields)
            {
                // Look for the definition of the additional field in the defined type 
                var correspondingField = type.AdditionalFields.FirstOrDefault(field => field.Name == additionalField.Name);
                if (correspondingField == null)
                {
                    throw new NoSuchAdditionalFieldException($"There's no field named {additionalField.Name} for type with id {type.Id})");
                }

                // Validate provided fields have the correct value type 
                switch (correspondingField.FieldType)
                {
                    case Domain.Fields.FieldType.Date:
                        DateTime parseDate;
                        if (!DateTime.TryParse(additionalField.Value, out parseDate))
                        {
                            throw new InvalidFieldValueCastForFieldTypeException($"value '{additionalField.Value}' for field with name {additionalField.Name} cannot be read as a date");
                        }

                        // is in range?
                        break;

                    case Domain.Fields.FieldType.Integer:
                        int num;
                        if (!int.TryParse(additionalField.Value, out num))
                        {
                            throw new InvalidFieldValueCastForFieldTypeException($"value '{additionalField.Value}' for field with name {additionalField.Name} cannot be read as an integer");
                        }

                        correspondingField.ValidateRange(num);
                        break;

                    case Domain.Fields.FieldType.Text:
                        if (string.IsNullOrEmpty(additionalField.Value) || string.IsNullOrWhiteSpace(additionalField.Value))
                        {
                            throw new InvalidFieldValueCastForFieldTypeException($"value for field with name {additionalField.Name} cannot be empty or null");
                        }

                        correspondingField.ValidateRange(additionalField.Value);
                        break;

                    default:
                        break;
                }

                // validar que los fields esten en rango
                
            }

            // validar que no falten campos obligatorios
            var requiredFields = type.AdditionalFields.Where(af => af.IsRequired).Select(af => af.Name);
            var providedFields = createRequest.AdditionalFields.Select(x => x.Name);
            var nonProvidedRequiredFields = requiredFields.Except<string>(providedFields, StringComparer.InvariantCultureIgnoreCase);
            if (nonProvidedRequiredFields.Any())
            {
                throw new LessAdditionalFieldsThanRequiredException($"Required fields {string.Join(", ", nonProvidedRequiredFields.ToArray())} should have been provided");
            }

            var request = new Request
            {
                Citizen = new Citizen { Email = createRequest.Email, Name = createRequest.Name, PhoneNumber = createRequest.Phone },
                Details = createRequest.Details,
                Type = type,
                FieldValues = new System.Collections.Generic.List<RequestField>()
            };

            // Do the field conversion for each provided field into the new request
            _requestRepo.Add(request);

            return request.Id;
        }


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

        #region Validation Methods

        private void ValidateTypeNotNull(CreateRequest createRequest, Domain.Type type)
        {
            if (type == null)
            {
                throw new NoSuchTopicException($"No topic with id={createRequest.TopicId} exists");
            }
        }

        private void ValidateRequestNotNull(int requestId, Request request)
        {
            if (request == null)
            {
                throw new NoSuchRequestException($"No request with id={requestId}");
            }
        }

        private void ValidateRequestId(int requestId)
        {
            if (requestId <= 0)
            {
                throw new InvalidGetRequestStatusException($"A valid request id should be greater than zero");
            }
        }
        #endregion Validation Methods
    }
}
