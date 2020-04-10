using Microsoft.EntityFrameworkCore;
using Type = IMMRequest.Domain.Type;

namespace IMMRequest.DataAccess.Repositories
{
    public class TypeRepository : Repository<Type>
    {
        public TypeRepository(DbContext context) : base(context)
        {
        }
    }
}
