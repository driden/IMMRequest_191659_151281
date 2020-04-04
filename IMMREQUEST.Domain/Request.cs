using System;

namespace IMMRequest.Domain
{

    public enum Status { Created, InReview, Accepted, Denied, Done }

    public class Request
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public string Details { get; set; } /* less 2000 */ 
        public Citizen Citizen { get; set; }
        public Type Type { get; set; }

        public Request()
        {

        }
    }
}