namespace IMMRequest.Domain.Fields
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;

    public class IntegerField : AdditionalField
    {
        public virtual IList<IntegerItem> Range { get; set; } = new List<IntegerItem>();

        public void AddToRange(int item)
        {
            (Range as IList<IntegerItem>).Add(new IntegerItem { Value = item });
        }

        public IntegerField()
        {
            FieldType = FieldType.Integer;
            IsRequired = false;
        }

        public override void ValidateRange<T>(IEnumerable<T> values)
        {
            ValidateRangeIsCorrect();
            if (Range.Count() == 2)
            {
                int[] val = values.Cast<int>().ToArray();
                int first = Range.First().Value;
                int second = Range.Skip(1).First().Value;

                if (val.All(v => first > v || v > second))
                {
                    throw new InvalidFieldRangeException($"One of the integer values {string.Join(',',val)} is not in range [{first},{second}]");
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
                int first = Range.First().Value;
                int second = Range.Skip(1).First().Value;
                if (first >= second)
                {
                    throw new InvalidFieldRangeException("Integer fields in range should be in ascending order");
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

        public override IEnumerable<string> GetRangeAsText()
        {
            return Range.Select(intItem => intItem.Value.ToString());
        }

        public override string GetTypeName() => "integer";
    }
}