using System;
using System.Linq;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class AdminRepository : Repository<Admin>
    {
        private readonly DbContext dbContext;
        private readonly DbSet<Admin> dbSetAdmin;

        public AdminRepository(DbContext context) : base(context)
        {
            this.dbContext = context;
            this.dbSetAdmin = dbContext.Set<Admin>();
        }
    }
}
