using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Repositories
{
    public class AreaRepository : Repository<Area>
    {
        public AreaRepository(DbContext context) : base(context) { }
    }
}
