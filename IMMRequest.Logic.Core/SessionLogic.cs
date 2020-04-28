using System;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Logic.Interfaces;

namespace IMMRequest.Logic.Core
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Admin> adminRepository;

        public SessionLogic(IRepository<Admin> _adminRepository)
        {
            this.adminRepository = _adminRepository;
        }

        public Guid Login(string userName, string password)
        {
            try
            {
                var admin = this.adminRepository.Get(a => a.Email.Equals(userName) && a.Password.Equals(password));
                if (admin.Token == Guid.Empty)
                {
                    admin.Token = Guid.NewGuid();
                    this.adminRepository.Update(admin);
                    this.adminRepository.SaveChanges();
                }

                return admin.Token;

            }
            catch (Exception)
            {
                throw new Exception("Invalid credentials");
            }
        }

        public bool IsValidToken(Guid token)
        {
            return this.adminRepository.Exist(a => a.Token == token);
        }
    }
}
