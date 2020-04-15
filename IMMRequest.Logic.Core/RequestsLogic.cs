using System;
using System.Collections.Generic;
using System.Text;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
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
            var request = new Request
            {
                Citizen = new Citizen(),
                Details = createRequest.Details,
                Topic = _topicRepo.Get(createRequest.TopicId),
            };

            _requestRepo.Add(request);
        }
    }
}
