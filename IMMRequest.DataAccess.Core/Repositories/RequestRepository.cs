namespace IMMRequest.DataAccess.Core.Repositories
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class RequestRepository : Repository<Request>
    {
        public RequestRepository(DbContext context) : base(context) { }
    }
}
