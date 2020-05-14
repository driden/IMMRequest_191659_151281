using System;
using IMMRequest.Domain;

namespace IMMRequest.DataAccess
{
    public interface IAdminQueries
    {
        bool Exists(Func<Admin, bool> predicate);
    }
}
