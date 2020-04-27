namespace IMMRequest.Logic.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IRequestsLogic
    {
        int Add(CreateRequest createRequest);
        public IEnumerable<GetAllRequestsStatusResponse> GetAllRequests();
    }
}
