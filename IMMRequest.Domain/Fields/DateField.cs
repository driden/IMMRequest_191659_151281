namespace IMMRequest.Domain.Fields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class DateField : AdditionalField
    {
        public virtual IEnumerable<DateItem> Range { get; set; } = new List<DateItem>();

        public void AddToRange(DateTime item)
        {
            (Range as IList<DateItem>).Add(new DateItem { Value = item });
        }

        public DateField()
        {
            FieldType = FieldType.Date;
            IsRequired = false;
        }

        public override void ValidateRange(object value)
        {
            if (Range.Count() != 0 && Range.Count() != 2)
            {
                throw new InvalidFieldRangeException("Integer fields can have 0 or 2 items");
            }

            if (Range.Count() == 2)
            {
                DateTime val = (DateTime)value;
                DateTime first = Range.First().Value;
                DateTime second = Range.Skip(1).First().Value;
                if (first >= second)
                {
                    throw new InvalidFieldRangeException("Date fields in range should be in ascending order");
                }

                if (first > val || val > second)
                {
                    throw new InvalidFieldRangeException($"Date value {ToDateString(val)} is not in range [{ToDateString(first)},{ToDateString(second)}]");
                }
            }
        }

        public override void AddToRange(IItem item)
        {
            if (item.Type == FieldType.Date)
            {
                AddToRange(((DateItem)item).Value);
            }
        }

        private string ToDateString(DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }
    }
}
