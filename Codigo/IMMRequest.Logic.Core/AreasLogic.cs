using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Logic.Core
{
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Models.Area;

    public class AreasLogic
    {
        private readonly IRepository<Area> _repository;

        public AreasLogic(IRepository<Area> areaRepository)
        {
            this._repository = areaRepository;
        }

        public IEnumerable<AreaModel> GetAll()
        {
            return _repository
                .GetAll()
                .Select(area =>
                    new AreaModel
                    {
                        Id = area.Id,
                        Name = area.Name,
                        Topics = area.Topics.Select(topic => topic.Id)
                    });
        }
    }
}
