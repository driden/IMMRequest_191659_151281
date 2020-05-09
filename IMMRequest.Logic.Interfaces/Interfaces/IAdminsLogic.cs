namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Domain;
    using Models;

    public interface IAdminsLogic
    {
        int Add(CreateAdminRequest request);
        void Remove(int adminId);
        void Update(int adminId, CreateAdminRequest request);
        IEnumerable<Admin> GetAll();
    }
}
