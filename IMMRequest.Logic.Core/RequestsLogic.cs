using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Logic.Exceptions;
using IMMRequest.Logic.Interfaces;
using IMMRequest.Logic.Models;

namespace IMMRequest.Logic.Core
{
    public class RequestsLogic : IRequestsLogic
    {
        private readonly IRepository<Request> _requestRepo;
        private readonly IRepository<Type> _typeRepo;
        private readonly IAreaQueries _areaQueries;

        public RequestsLogic(
               IRepository<Request> requestRepository,
               IRepository<Type> typeRepository,
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

            // si el topic existe aca
            // traigo la lista de additional fields
            // validar que los fields tengan tipos correctos
            // validar que los fields esten en rango
            // validar que no falten campos obligatorios

            // Persistir la lista de fields en la base

            var request = new Request
            {
                Citizen = new Citizen { Email = createRequest.Email, Name = createRequest.Name, PhoneNumber = createRequest.Phone },
                Details = createRequest.Details,
                Type = type,
            };

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

        private void ValidateTypeNotNull(CreateRequest createRequest, Type type)
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
