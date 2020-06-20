namespace IMMRequest.Domain.Fields
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class BooleanField : AdditionalField
    {
        public virtual IList<BooleanItem> Range { get; set; } =
            new List<BooleanItem>(
                new[]
                {
                    new BooleanItem { Value = true },
                    new BooleanItem { Value = false }
                });

        public BooleanField()
        {
            FieldType = FieldType.Boolean;
            IsRequired = false;
        }

        public override void ValidateRange<T>(IEnumerable<T> values)
        {
            if (values is null)
            {
                throw new InvalidFieldRangeException($"a boolean additional field cannot be null");
            }
        }

        public override void ValidateRangeIsCorrect()
        {
        }

        public override void AddToRange(IItem item)
        {
        }

        public override IEnumerable<string> GetRangeAsText()
        {
            return Range.Select(x => x.ToString());
        }

        public override string GetTypeName() => "boolean";
    }
}
