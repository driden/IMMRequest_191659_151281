using System;
using IMMRequest.DataAccess.Interfaces;
using IMMRequest.Domain;
using IMMRequest.Logic.Interfaces;

namespace IMMRequest.Logic.Core
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Admin> _adminRepository;

        public SessionLogic(IRepository<Admin> adminRepository)
        {
            this._adminRepository = adminRepository;
        }

        public Guid Login(string userName, string password)
        {
            try
            {
                var admin = this._adminRepository.FirstOrDefault(a => a.Email.Equals(userName)
                                                                      && a.Password.Equals(password));
                if (admin.Token == null || admin.Token == Guid.Empty)
                {
                    admin.Token = Guid.NewGuid();
                    this._adminRepository.Update(admin);
                }

                return admin.Token;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Invalid credentials");
            }
        }

        public bool IsValidToken(Guid token, string username)
        {
            return this._adminRepository.Exists(a => a.Token == token && a.Email == username);
        }
    }
}
