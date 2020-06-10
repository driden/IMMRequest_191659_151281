namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using IMMRequest.Domain;
    
    public interface IReportsLogic
    {
        IEnumerable<Request> GetRequestByMail(string mail);
    }
}
