namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models;
    using Models.Request;

    public interface IRequestsLogic
    {
        int Add(CreateRequest createRequest);
        IEnumerable<RequestStatusModel> GetAllRequests();
        RequestModel GetRequestStatus(int requestId);
        void UpdateRequestStatus(int requestId, string newState);
    }
}
