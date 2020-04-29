namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IRequestsLogic
    {
        int Add(CreateRequest createRequest);
        IEnumerable<GetAllRequestsStatusResponse> GetAllRequests();
        void UpdateRequestStatus(int requestId, string newState);
    }
}
