using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class UserRepository: Repository<User>
    {
        public UserRepository(DbContext context): base(context) { }
    }
}
