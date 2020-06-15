namespace IMMRequest.Logic.Core
{
    using System.Collections.Generic;
    using DataAccess.Interfaces;
    using Domain;
    using Domain.Exceptions;
    using Exceptions.Account;
    using Interfaces;
    using Models.Admin;


    public class AdminsLogic : IAdminsLogic
    {
        private readonly IRepository<Admin> _adminRepository;

        public AdminsLogic(IRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public int Add(AdminModel request)
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

        public void Update(int adminId, AdminModel request)
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
        }

        public IEnumerable<Admin> GetAll()
        {
            return _adminRepository.GetAll();
        }

        public void Remove(int adminId)
        {
            ValidateAdminId(adminId);
            var storedAdmin = _adminRepository.Get(adminId);
            ValidateExistingAdmin(storedAdmin);

            _adminRepository.Remove(storedAdmin);
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
                throw new InvalidAdminIdException("Admin with given Id couldn't be found");
            }
        }

        public void ValidateEmailIsNotUsedByAnotherAdmin(string email, int adminId)
        {
            if (!ExistsAdminWithEmail(email)) return;
            var emailOwner =
                _adminRepository.FirstOrDefault(f => f.Email.Equals(email));
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
                throw new InvalidAdminIdException("An admin can't have an Id lower than 1");
            }
        }

        private bool ExistsAdminWithEmail(string email)
        {
            return _adminRepository.Exists(a => a.Email.Equals(email));
        }
        #endregion

    }
}
