namespace IMMRequest.DataAccess.Core.Repositories
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}
