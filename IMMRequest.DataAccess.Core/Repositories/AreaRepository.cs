using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class AreaRepository : Repository<Area>, IAreaQueries
    {
        public AreaRepository(DbContext context) : base(context) { }

        public Area FindWithTopicId(int topicId)
        {
            return FirstOrDefault(area =>
                area.Topics.Any(
                    topic => topic.Id == topicId)
                );
        }
    }
}
