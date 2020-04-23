using IMMRequest.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.Domain.Fields
{
    public class DateField : AdditionalField
    {
        public virtual IEnumerable<DateItem> Range { get; set; } = new List<DateItem>();

        public void AddToRange(DateTime item)
        {
            (this.Range as IList<DateItem>).Add(new DateItem { Value = item });
        }

        public DateField()
        {
            this.FieldType = FieldType.Date;
        }

        public override void ValidateRange(object value)
        {
            if (Range.Count() != 0 && Range.Count() != 2)
            {
                throw new InvalidFieldRangeException("Integer fields can have 0 or 2 items");
            }
            else
            {
                if (Range.Count() == 2)
                {
                    DateTime val = (DateTime)value;
                    DateTime first = Range.First().Value;
                    DateTime second = Range.Skip(1).First().Value;
                    if (first >= second)
                    {
                        throw new InvalidFieldRangeException("Date fields in range should be in ascending order");
                    }
                    else if (first > val || val > second)
                    {
                        throw new InvalidFieldRangeException($"Date value {ToDateString(val)} is not in range [{ToDateString(first)},{ToDateString(second)}]");
                    }
                }
            }
        }

        public override void AddToRange(IItem item)
        {
            if (item.Type == FieldType.Integer)
            {
                this.AddToRange(((DateItem)item).Value);
            }
        }

        private string ToDateString(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

    }
}
