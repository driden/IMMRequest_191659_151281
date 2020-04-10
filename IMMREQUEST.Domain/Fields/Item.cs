using System;

namespace IMMRequest.Domain.Fields
{
    public class DateItem : IItem
    {
        public int Id { get; set; }
        public DateTime Value { get; set; }
        public int DateFieldId { get; set; }
        public FieldType Type => FieldType.Date; 
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

    public interface IItem
    {
        public FieldType Type { get; }
    }
}
