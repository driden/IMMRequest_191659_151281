using System;
namespace IMMRequest.Logic.Interfaces
{
    public interface ISessionLogic
    {
        Guid Login(string userName, string password);
        bool IsValidToken(Guid token, string username);
    }
}
