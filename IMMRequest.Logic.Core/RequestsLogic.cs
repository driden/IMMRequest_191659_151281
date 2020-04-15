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
        private readonly IRequestRepository _requestRepo;
        private readonly IAreaRepository _areaRepo;

        public RequestsLogic(
               IRequestRepository requestRepository,
               IAreaRepository areaRepository)
        {
            this._requestRepo = requestRepository;
            this._areaRepo = areaRepository;
        }

        public void Add(CreateRequest createRequest)
        {
            var request = new Request
            {
                Area = _areaRepo.Get(createRequest.AreaId),
                Citizen = new Citizen(),
                Details = createRequest.Details,
                Topic = new Topic(),
                Type = new Domain.Type()
            };

            _requestRepo.Add(request);
        }
    }
}
