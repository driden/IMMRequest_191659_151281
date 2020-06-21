namespace IMMRequest.Domain.Fields
{
    using System;

    public class DateItem : IItem
    {
        public int Id { get; set; }
        public DateTime Value { get; set; }
        public int DateFieldId { get; set; }
        public FieldType Type => FieldType.Date;
        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd");
        }
    }

    public class TextItem : IItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int TextFieldId { get; set; }
        public FieldType Type => FieldType.Text;
    }

    public class IntegerItem : IItem
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int IntegerFieldId { get; set; }
        public FieldType Type => FieldType.Integer;
    }

    public class BooleanItem : IItem
    {
        public int Id { get; set; }
        public bool Value { get; set; }
        public int BooleanFieldId { get; set; }
        public FieldType Type => FieldType.Boolean;
    }

    public interface IItem
    {
        FieldType Type { get; }
    }
}
