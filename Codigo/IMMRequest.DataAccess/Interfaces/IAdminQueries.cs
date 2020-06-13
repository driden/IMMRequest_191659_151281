namespace IMMRequest.DataAccess.Interfaces
{
    using System;
    using Domain;

    public interface IAdminQueries
    {
        bool Exists(Func<Admin, bool> predicate);
    }
}
