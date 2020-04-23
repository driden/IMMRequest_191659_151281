using IMMRequest.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMMRequest.Domain.Fields
{
    public class TextField : AdditionalField
    {
        public virtual IEnumerable<TextItem> Range { get; set; } = new List<TextItem>();

        public void AddToRange(string item)
        {
            (this.Range as IList<TextItem>).Add(new TextItem { Value = item });

        }

        public TextField()
        {
            this.FieldType = FieldType.Text;
        }

        public override void ValidateRange(object value)
        {
            if (Range.Any() && !Range.Any(rangeVal => value.ToString().Trim().Equals(rangeVal)))
            {
                throw new InvalidFieldRangeException($"text '{value}' is not in range {string.Join(", ", Range.Select(r => r.Value).ToArray())}");
            }
        }

        public override void AddToRange(IItem item)
        {
            if (item.Type == FieldType.Integer)
            {
                this.AddToRange(((TextItem)item).Value);
            }
        }
    }
}
