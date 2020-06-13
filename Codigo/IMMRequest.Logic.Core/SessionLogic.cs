namespace IMMRequest.Logic.Core
{
    using System;
    using DataAccess.Interfaces;
    using Domain;
    using Interfaces;
    using Exceptions;
    using Models.Admin;

    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Admin> _adminRepository;

        public SessionLogic(IRepository<Admin> adminRepository)
        {
            this._adminRepository = adminRepository;
        }

        public Guid Login(AdminLoginModel loginInfo)
        {
            var userName = loginInfo.Email;
            var password = loginInfo.Password;

            var admin = this._adminRepository.FirstOrDefault(a => a.Email.Equals(userName)
                                                                  && a.Password.Equals(password));
            if (admin != null && admin.Token == Guid.Empty)
            {
                admin.Token = Guid.NewGuid();
                this._adminRepository.Update(admin);
            }
            else if (admin is null)
            {
                throw new NoSuchAdministrator("Invalid Credentials");
            }

            return admin.Token;
        }

        public bool IsValidToken(Guid token, string username)
        {
            return this._adminRepository.Exists(a => a.Token == token && a.Email == username);
        }
    }
}
