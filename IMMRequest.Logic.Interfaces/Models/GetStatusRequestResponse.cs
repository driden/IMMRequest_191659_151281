using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Logic.Models
{
    public class GetStatusRequestResponse
    {
        public string Details  { get; set; }
        public string RequestState { get; set; }
        public string CitizenName { get; set; }
        public string CitizenPhoneNumber { get; set; }
        public string CitizenEmail { get; set; }
        public string Area { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
    }
}
