using System.Linq;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Repositories
{
    public class RequestRepository : Repository<Request>
    {
        public RequestRepository(DbContext context) : base(context) { }

        public Request GetWithAreaAndType(int id)
        {
            return
            Context.Set<Request>().Include(x => x.Area)
                .Include(x => x.Type)
                .Include(x => x.Topic)
                .FirstOrDefault(request => request.Id == id);
        }
    }
}
