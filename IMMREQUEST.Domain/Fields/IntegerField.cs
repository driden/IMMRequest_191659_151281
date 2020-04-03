using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class IntegerField : Field
    {
        public int Type { get; set; }
        public IEnumerable<Item<int>> Range { get; set; }

        public IntegerField() : base()
        {
        }
    }
}
