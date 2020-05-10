namespace IMMRequest.Logic.Models
{
    public class GetAllRequestsStatusResponse
    {
        public int RequestId { get; set; }

        public string RequestedBy { get; set; }

        public string Status { get; set; }

        public string Details { get; set; }
    }
}
