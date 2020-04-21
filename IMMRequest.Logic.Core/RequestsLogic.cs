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
        private readonly IRepository<Topic> _topicRepo;

        public RequestsLogic(
               IRepository<Request> requestRepository,
               IRepository<Topic> topicRepository
            )
        {
            this._requestRepo = requestRepository;
            this._topicRepo = topicRepository;
        }

        public int Add(CreateRequest createRequest)
        {
            var topic = _topicRepo.Get(createRequest.TopicId);
            if (topic == null)
            {
                throw new NoSuchTopicException($"No topic with id={createRequest.TopicId} exists");
            }

            var request = new Request
            {
                Citizen = new Citizen { Email = createRequest.Email, Name = createRequest.Name, PhoneNumber = createRequest.Phone },
                Details = createRequest.Details,
                Topic = topic,
            };

            _requestRepo.Add(request);

            return request.Id;
        }

        public GetStatusRequestResponse GetRequestStatus(int requestId)
        {
            ValidateRequestId(requestId);

            var request = this._requestRepo.Get(requestId);

            return new GetStatusRequestResponse
            {
                Details = request.Details,
                RequestState = request.Status.Description,
                CitizenName = request.Citizen.Name,
                CitizenPhoneNumber = request.Citizen.PhoneNumber,
                CitizenEmail = request.Citizen.Email
            };
        }

        private void ValidateRequestId(int requestId)
        {
            if (requestId <= 0)
            {
                throw new InvalidGetRequestStatusException($"A valid request id should be greater than zero");
            }
        }
    }
}
