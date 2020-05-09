namespace IMMRequest.Logic.Core
{
    using System;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Exceptions.RemoveType;
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

            ValidateEmailIsNotUsed(admin);
            _adminRepository.Add(admin);
            return admin.Id;
        }

        public int Update(int adminId, CreateAdminRequest request)
        {
            ValidateAdminId(adminId);
            var admin = new Admin
            {
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Password = request.Password
            };

            ValidateEmailIsNotUsedByAnotherAdmin(admin.Email, adminId);

            var storedAdmin = _adminRepository.Get(adminId);
            ValidateExistingAdmin(storedAdmin);

            UpdateAdminFields(storedAdmin, admin);
            _adminRepository.Update(storedAdmin);

            return admin.Id;
        }

        private void UpdateAdminFields(Admin oldAdmin, Admin updatedAdmin)
        {
            oldAdmin.Password = updatedAdmin.Password;
            oldAdmin.Name = updatedAdmin.Name;
            oldAdmin.PhoneNumber = updatedAdmin.PhoneNumber;
            oldAdmin.Email = updatedAdmin.Email;
        }

        #region Validations

        public void ValidateExistingAdmin(Admin storedAdmin)
        {
            if (storedAdmin is null)
            {
                throw new InvalidIdException("Admin with given Id couln't be found");
            }
        }

        public void ValidateEmailIsNotUsedByAnotherAdmin(string email, int adminId)
        {
            if (!ExistsAdminWithEmail(email)) return;
            var emailOwner =
                _adminRepository.FirstOrDefault(f => f.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (emailOwner.Id != adminId)
            {
                throw new InvalidEmailException("Cant use an email that's registered.");
            }
        }

        private void ValidateEmailIsNotUsed(Admin admin)
        {
            if (ExistsAdminWithEmail(admin.Email))
            {
                throw new InvalidEmailException("That email is already registered.");
            }
        }

        public void ValidateAdminId(int id)
        {
            if (id < 1)
            {
                throw new InvalidIdException("An admin can't have an Id lower than 1");
            }
        }

        private bool ExistsAdminWithEmail(string email)
        {
            return _adminRepository.Exists(a => email.Equals(a.Email, StringComparison.OrdinalIgnoreCase));
        }
        #endregion

    }
}
