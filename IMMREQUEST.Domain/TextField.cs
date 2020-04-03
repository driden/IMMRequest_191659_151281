using System;

namespace IMMRequest.Domain
{
    public class TextField : Field
    {
        public string Type { get; set; }

        public string[] Range { get; set; }
    }
}