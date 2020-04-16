using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Logic.Exceptions;
using IMMRequest.Logic.Models;

namespace IMMRequest.Logic.Core
{
    public class RequestsLogic
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

        public void Add(CreateRequest createRequest)
        {
            var topic = _topicRepo.Get(createRequest.TopicId);
            if (topic == null)
            {
                throw new NoSuchTopicException($"No topic with id={createRequest.TopicId} exists");
            }

            var request = new Request
            {
                Citizen = new Citizen(),
                Details = createRequest.Details,
                Topic = topic,
            };

            _requestRepo.Add(request);
        }
    }
}
