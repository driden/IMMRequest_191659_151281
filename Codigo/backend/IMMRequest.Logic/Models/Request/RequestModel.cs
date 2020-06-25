namespace IMMRequest.Logic.Models.Request
{
    using System.Collections.Generic;

    public class RequestModel
    {
        public string Details { get; set; }

        public string RequestState { get; set; }

        public string CitizenName { get; set; }

        public string CitizenPhoneNumber { get; set; }

        public string CitizenEmail { get; set; }

        public int RequestId { get; set; }

        public IEnumerable<FieldRequestModel> Fields { get; set; }
    }
}
