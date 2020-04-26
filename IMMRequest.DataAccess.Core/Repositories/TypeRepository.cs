namespace IMMRequest.DataAccess.Core.Repositories
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class TypeRepository : Repository<Type>
    {
        public TypeRepository(DbContext context) : base(context)
        {
        }
    }
}
