namespace IMMRequest.DataAccess.Core.Repositories
{
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class AreaRepository : Repository<Area>
    {
        public AreaRepository(DbContext context) : base(context) { }

        public Area FindWithTypeId(int typeId)
        {
            var type = Context.Set<Type>().Find(typeId);
            var topic = Context.Set<Topic>().Find(type.TopicId);
            return GetAll().FirstOrDefault(area => area.Id == topic.AreaId);
        }
    }
}
