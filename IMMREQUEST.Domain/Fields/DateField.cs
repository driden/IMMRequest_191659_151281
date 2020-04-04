using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class DateField : Field
    {
        public IEnumerable<Item<DateTime>> Range { get; set; }

        public override void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
