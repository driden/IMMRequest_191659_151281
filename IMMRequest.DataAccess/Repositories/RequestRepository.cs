using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Repositories
{
    public class RequestRepository:Repository<Area>
    {
        public RequestRepository(DbContext context): base(context) { }
    }
}
