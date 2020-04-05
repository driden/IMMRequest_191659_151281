using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class TextField : FieldBase
    {
        public IEnumerable<Item<string>> Range { get; set; }

        public override void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
