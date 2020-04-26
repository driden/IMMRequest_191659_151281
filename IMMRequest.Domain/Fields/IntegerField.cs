namespace IMMRequest.Domain.Fields
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class IntegerField : AdditionalField
    {
        public virtual IEnumerable<IntegerItem> Range { get; set; } = new List<IntegerItem>();

        public void AddToRange(int item)
        {
            (Range as IList<IntegerItem>).Add(new IntegerItem { Value = item });
        }

        public IntegerField()
        {
            FieldType = FieldType.Integer;
           IsRequired = false;
        }

        public override void ValidateRange(object value)
        {
            if (Range.Count() != 0 && Range.Count() != 2)
            {
                throw new InvalidFieldRangeException("Integer fields can have 0 or 2 items");
            }

            int val = (int)value;
            if (Range.Count() == 2)
            {
                int first = Range.First().Value;
                int second = Range.Skip(1).First().Value;
                if (first >= second)
                {
                    throw new InvalidFieldRangeException("Integer fields in range should be in ascending order");
                }

                if (first > val || val > second)
                {
                    throw new InvalidFieldRangeException($"Integer value {val} is not in range [{first},{second}]");
                }
            }
        }

        public override void AddToRange(IItem item)
        {
            if (item.Type == FieldType.Integer)
            {
                AddToRange(((IntegerItem)item).Value);
            }
        }
    }
}
