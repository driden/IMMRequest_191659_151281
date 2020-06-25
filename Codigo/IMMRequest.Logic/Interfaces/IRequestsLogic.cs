namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models.Request;

    public interface IRequestsLogic
    {
        int Add(CreateRequestModel createRequestModel);
        IEnumerable<RequestStatusModel> GetAllRequests();
        RequestModel GetRequestStatus(int requestId);
        void UpdateRequestStatus(int requestId, string newState);
    }
}
