using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class RequestArea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RequestTopic> Topics { get; set; }

        public RequestArea()
        {
            this.Topics = new List<RequestTopic>();
        }
    }
}
