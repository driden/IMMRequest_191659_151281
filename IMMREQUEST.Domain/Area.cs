using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }

        public Area()
        {
            this.Topics = new List<Topic>();
        }
    }
}
