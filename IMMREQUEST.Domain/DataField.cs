using System;

namespace IMMREQUEST.Domain
{
    public class DataField : Field
    {
        public DateTime Type { get; set; }

        public DateTime[] Range { get; set; }

        public DataField()
        {
            
        }
    }
}