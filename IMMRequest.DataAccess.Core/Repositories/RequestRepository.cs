using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMMRequest.DataAccess.Core.Repositories
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
    public RequestRepository(DbContext context) : base(context) { }
  }
}
