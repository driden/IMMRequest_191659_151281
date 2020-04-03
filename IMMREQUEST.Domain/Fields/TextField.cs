using System;

namespace IMMRequest.Domain.Fields
{
    public class TextField : Field
    {
        public string Type { get; set; }

        public string[] Range { get; set; }
    }
}
