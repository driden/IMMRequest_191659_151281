using System;
using System.Collections.Generic;

namespace IMMREQUEST.Domain
{
    public class RequestTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RequestType> Types { get; set; }

        public RequestTopic()
        {

        }
    }
}
