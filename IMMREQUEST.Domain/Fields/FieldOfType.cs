using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public abstract class FieldOfType<T> :  AdditionalField
    {
        public IEnumerable<Item<T>> Range { get; set; } = new List<Item<T>>();
        public T Value { get; set; } = default(T);

        public void AddToRange(T item)
        {
            (this.Range as IList<Item<T>>).Add(new Item<T> { Value = item });
        }

        public abstract void ValidateRange();
    }
}
