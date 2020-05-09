using System;
using System.Linq;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class AdminRepository : Repository<Admin>
    {
        public AdminRepository(DbContext context) : base(context)
        {
        }
    }
}
