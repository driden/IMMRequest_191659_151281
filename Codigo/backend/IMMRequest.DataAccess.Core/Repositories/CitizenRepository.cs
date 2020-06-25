namespace IMMRequest.DataAccess.Core.Repositories
{
    using Domain;
    using Microsoft.EntityFrameworkCore;

    public class CitizenRepository : Repository<Citizen>
    {
        public CitizenRepository(DbContext context) : base(context)
        {
        }
    }
}