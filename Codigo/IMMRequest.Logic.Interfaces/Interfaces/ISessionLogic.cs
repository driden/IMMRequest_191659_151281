using System;
namespace IMMRequest.Logic.Interfaces
{
    using Models;

    public interface ISessionLogic
    {
        Guid Login(ModelAdminLogin loginInfo);
        bool IsValidToken(Guid token, string username);
    }
}
