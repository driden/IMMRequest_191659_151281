using System;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Type> Types { get; set; }

        public Topic()
        {

        }
    }
}
