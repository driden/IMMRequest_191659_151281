using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
    public class DateField : AdditionalField
    {
        public IEnumerable<DateItem> Range { get; set; } = new List<DateItem>();
        public DateTime Value { get; set; } = default(DateTime);

        public void AddToRange(DateTime item)
        {
            (this.Range as IList<DateItem>).Add(new DateItem { Value = item });
        }

        public DateField()
        {
            this.FieldType = FieldType.Date;
        }

        public void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
