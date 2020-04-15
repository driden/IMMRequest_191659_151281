using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class AreaRepository : Repository<Area>
    {
        public AreaRepository(DbContext context): base(context) { }
    }
}
