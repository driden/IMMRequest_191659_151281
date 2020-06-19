namespace IMMRequest.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Models.State;

    public interface IReportsLogic
    {
        IEnumerable<StateReportModel> GetRequestByMail(string mail, DateTime startDate, DateTime endDate);
    }
}