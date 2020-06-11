namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IRequestsLogic
    {
        int Add(CreateRequest createRequest);
        IEnumerable<RequestStatusModel> GetAllRequests();
        GetStatusRequestResponse GetRequestStatus(int requestId);
        void UpdateRequestStatus(int requestId, string newState);
    }
}
