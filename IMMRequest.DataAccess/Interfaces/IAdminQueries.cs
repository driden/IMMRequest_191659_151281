using System;
using IMMRequest.Domain;

namespace IMMRequest.DataAccess
{
    public interface IAdminQueries
    {
        bool Exist(Func<Admin, bool> predicate);
    }
}
