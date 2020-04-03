using System;

namespace IMMREQUEST.Domain
{
    public class TextField : Field
    {
        public String Type { get; set; }

        public String[] Range { get; set; }
    }
}