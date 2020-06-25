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
        private readonly IRepository<Area> _areaRepository;

        public AreasLogic(IRepository<Area> areaAreaRepository)
        {
            _areaRepository = areaAreaRepository;
        }

        public IEnumerable<AreaModel> GetAll()
        {
            return _areaRepository
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
