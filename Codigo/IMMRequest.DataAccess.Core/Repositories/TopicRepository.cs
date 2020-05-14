namespace IMMRequest.DataAccess.Core.Repositories
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class TopicRepository : Repository<Topic>
    {
        public TopicRepository(DbContext context) : base(context) { }

    }
}
