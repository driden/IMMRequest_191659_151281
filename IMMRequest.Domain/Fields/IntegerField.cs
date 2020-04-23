using IMMRequest.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.Domain.Fields
{
    public class IntegerField : AdditionalField
    {
        public virtual IEnumerable<IntegerItem> Range { get; set; } = new List<IntegerItem>();

        public void AddToRange(int item)
        {
            (this.Range as IList<IntegerItem>).Add(new IntegerItem { Value = item });
        }

        public IntegerField()
        {
            this.FieldType = FieldType.Integer;
        }

        public override void ValidateRange(object value)
        {
            if (Range.Count() != 0 && Range.Count() != 2)
            {
                throw new InvalidFieldRangeException("Integer fields can have 0 or 2 items");
            }
            else
            {
                int val = (int)value;
                if (Range.Count() == 2)
                {
                    int first = Range.First().Value;
                    int second = Range.Skip(1).First().Value;
                    if (first >= second)
                    {
                        throw new InvalidFieldRangeException("Integer fields in range should be in ascending order");
                    }
                    else if (first > val || val > second)
                    {
                        throw new InvalidFieldRangeException($"Integer value {val} is not in range [{first},{second}]");
                    }
                }
            }
        }

        public override void AddToRange(IItem item)
        {
            if (item.Type == FieldType.Integer)
            {
                this.AddToRange(((IntegerItem)item).Value);
            }
        }
    }
}
