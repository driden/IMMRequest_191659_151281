using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public abstract class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRequired { get; set; }

        public abstract void ValidateRange();
    }
}
