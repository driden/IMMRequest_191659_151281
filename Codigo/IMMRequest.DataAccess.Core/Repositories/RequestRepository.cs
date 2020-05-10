namespace IMMRequest.DataAccess.Core.Repositories
{
    using System.Linq;
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class RequestRepository : Repository<Request>
    {
        public RequestRepository(DbContext context) : base(context) { }

        public override void Add(Request entity)
        {
            var user = Context.Set<Citizen>().FirstOrDefault(u => u.Email == entity.Citizen.Email);
            if (user != null)
            {
                entity.Citizen = user;
            }

            Context.Set<Request>().Add(entity);
            Context.SaveChanges();

        }
    }
}
