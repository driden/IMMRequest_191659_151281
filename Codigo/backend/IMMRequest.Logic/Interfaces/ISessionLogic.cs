namespace IMMRequest.Logic.Interfaces
{
    using System;
    using Models.Admin;

    public interface ISessionLogic
    {
        Guid Login(AdminLoginModel loginInfo);
        bool IsValidToken(Guid token, string username);
    }
}
