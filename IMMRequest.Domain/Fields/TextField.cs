namespace IMMRequest.Domain.Fields
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class TextField : AdditionalField
    {
        public virtual IEnumerable<TextItem> Range { get; set; } = new List<TextItem>();

        public void AddToRange(string item)
        {
            (Range as IList<TextItem>).Add(new TextItem { Value = item });

        }

        public TextField()
        {
            FieldType = FieldType.Text;
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
            if (item.Type == FieldType.Text)
            {
                AddToRange(((TextItem)item).Value);
            }
        }
    }
}
