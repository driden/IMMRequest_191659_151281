namespace IMMRequest.Logic.Models
{
    using System.Collections.Generic;

    public class GetStatusRequestResponse
    {
        public string Details  { get; set; }

        public string RequestState { get; set; }

        public string CitizenName { get; set; }

        public string CitizenPhoneNumber { get; set; }

        public string CitizenEmail { get; set; }

        public IEnumerable<FieldRequestModel> Fields { get; set; }
    }
} 
