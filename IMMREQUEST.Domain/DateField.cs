using IMMRequest.Domain.Fields;
using System;

namespace IMMRequest.Domain
{
    public class DateField : Field
    {
        public DateTime Type { get; set; }

        public DateTime[] Range { get; set; }

        public DateField()
        {

        }
    }
}
