using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public abstract class FieldOfType<T> :  AdditionalField
    {
        public IEnumerable<Item<T>> Range { get; set; }
        public T Value { get; set; }
        public abstract void ValidateRange();
    }
}
