using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class DateField : Field
    {
        public DateTime Type { get; set; }

        public List<Item<DateTime>> Range { get; set; }

        public DateField() : base()
        {
        }
    }
}
