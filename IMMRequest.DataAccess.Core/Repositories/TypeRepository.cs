using Microsoft.EntityFrameworkCore;
using Type = IMMRequest.Domain.Type;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class TypeRepository : Repository<Type>
    {
        public TypeRepository(DbContext context) : base(context)
        {
        }
    }
}
