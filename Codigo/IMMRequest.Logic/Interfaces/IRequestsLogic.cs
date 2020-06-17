namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models.Request;
    using Models.State;

    public interface IRequestsLogic
    {
        int Add(CreateRequestModel createRequestModel);
        IEnumerable<RequestStatusModel> GetAllRequests();
        RequestModel GetRequestStatus(int requestId);
        void UpdateRequestStatus(int requestId, string newState);
        IEnumerable<StateReportModel> GetRequestByMail(string mail);
    }
}
