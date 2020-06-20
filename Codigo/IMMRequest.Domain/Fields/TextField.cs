namespace IMMRequest.Domain.Fields
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class TextField : AdditionalField
    {
        public virtual IList<TextItem> Range { get; set; } = new List<TextItem>();

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
            ValidateRangeIsCorrect();
            if (Range.Any() && !Range.Any(rangeVal => value.ToString().Trim().Equals(rangeVal)))
            {
                throw new InvalidFieldRangeException($"text '{value}' is not in range {string.Join(", ", Range.Select(r => r.Value).ToArray())}");
            }
        }

        public override void ValidateRangeIsCorrect()
        {
            if (Range.Any(x => string.IsNullOrWhiteSpace(x.Value)))
            {
                throw new InvalidFieldRangeException("At least one of your types range is an empty string");
            }
        }

        public override void AddToRange(IItem item)
        {
            if (item.Type == FieldType.Text)
            {
                AddToRange(((TextItem)item).Value);
            }
        }

        public override IEnumerable<string> GetRangeAsText()
        {
            return Range.Select(textItem => textItem.Value);
        }

        public override string GetTypeName() => "text";
    }
}
