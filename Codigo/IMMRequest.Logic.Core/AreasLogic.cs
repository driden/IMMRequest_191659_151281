namespace IMMRequest.Logic.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using DataAccess.Interfaces;
    using Domain;
    using Interfaces;
    using Models.Area;

    public class AreasLogic : IAreasLogic
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
