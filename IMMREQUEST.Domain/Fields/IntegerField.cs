using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class IntegerField: FieldBase
    {
        public IEnumerable<Item<int>> Range { get; set; }

        public override void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
