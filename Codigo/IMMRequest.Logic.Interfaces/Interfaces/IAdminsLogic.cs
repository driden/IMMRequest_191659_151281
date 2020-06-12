namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Domain;
    using Models.Admin;

    public interface IAdminsLogic
    {
        int Add(AdminModel request);
        void Remove(int adminId);
        void Update(int adminId, AdminModel request);
        IEnumerable<Admin> GetAll();
    }
}
