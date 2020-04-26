namespace IMMRequest.DataAccess.Core.Repositories
{
    using System.Linq;
    using Domain;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class AreaRepository : Repository<Area>, IAreaQueries
    {
        public AreaRepository(DbContext context) : base(context) { }

        public Area FindWithTypeId(int typeId)
        {
            var type = Context.Set<Type>().Find(typeId);
            var topic = Context.Set<Topic>().Find(type.TopicId);
            var area = GetAll().FirstOrDefault(area => area.Id == topic.AreaId);
            return area;
        }
    }
}
