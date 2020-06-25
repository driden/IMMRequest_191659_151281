namespace IMMRequest.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Models.State;
    using Models.Type;

    public interface IReportsLogic
    {
        IEnumerable<StateReportModel> GetRequestByMail(string mail, DateTime startDate, DateTime endDate);
        IEnumerable<TypeReportModel> GetMostUsedTypes(DateTime startDate, DateTime endDate);
    }
}
