namespace IMMRequest.Logic.Core
{
    using System;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Models;

    public class AdminsLogic
    {
        private readonly IRepository<Admin> _adminRepository;

        public AdminsLogic(IRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public int Add(CreateAdminRequest request)
        {
            var admin = new Admin
            {
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password
            };

            if (_adminRepository.Exists(a => admin.Email.Equals(a.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidEmailException("That email is already registered.");
            }

            _adminRepository.Add(admin);
            return admin.Id;
        }
    }
}
