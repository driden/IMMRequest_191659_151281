using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Repositories
{
    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(DbContext context) : base(context) { }

    }
}
