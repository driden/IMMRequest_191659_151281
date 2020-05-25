using System.Collections.Generic;
using IMMRequest.Logic.Models;

namespace IMMRequest.Logic.Interfaces
{
    public interface IReportsLogic
    {
        IEnumerable<GetAllRequestsByMail> ReportsRequestByMail(string mail);
    }
}
