using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class RequestRepository : Repository<Request>
    {
        public RequestRepository(DbContext context) : base(context) { }
    }
}
