namespace IMMRequest.Domain.Fields
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class DateField : AdditionalField
    {
        public virtual IList<DateItem> Range { get; set; } = new List<DateItem>();

        public void AddToRange(DateTime item)
        {
            (Range as IList<DateItem>).Add(new DateItem { Value = item });
        }

        public DateField()
        {
            FieldType = FieldType.Date;
            IsRequired = false;
        }

        public override void ValidateRange<T>(IEnumerable<T> values)
        {
            ValidateRangeIsCorrect();
            if (Range.Count() == 2)
            {
                DateTime[] val = values.Cast<DateTime>().ToArray();
                DateTime first = Range.First().Value;
                DateTime second = Range.Skip(1).First().Value;

                if (val.All(v => first > v || v > second))
                {
                    var dateValues = string.Join(',', val.Select(ToDateString));
                    throw new InvalidFieldRangeException($"One of the date values {dateValues} is not in range [{ToDateString(first)},{ToDateString(second)}]");
                }
            }
        }

        public override void ValidateRangeIsCorrect()
        {
            if (Range.Count() != 0 && Range.Count() != 2)
            {
                throw new InvalidFieldRangeException("Integer fields can have 0 or 2 items");
            }

            if (Range.Count() == 2)
            {
                DateTime first = Range.First().Value;
                DateTime second = Range.Skip(1).First().Value;
                if (first >= second)
                {
                    throw new InvalidFieldRangeException("Date fields in range should be in ascending order");
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

        public override IEnumerable<string> GetRangeAsText()
        {
            return Range.Select(dateItem => dateItem.ToString());
        }

        public override string GetTypeName() => "date";

        private string ToDateString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
