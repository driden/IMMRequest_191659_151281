using System;

namespace IMMRequest.Domain.Fields
{
    public class DateItem
    {
        public int Id { get; set; }
        public DateTime Value { get; set; }
        public int DateFieldId { get; set; }
    }

    public class TextItem
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int TextFieldId { get; set; }
    }

    public class IntegerItem
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int IntegerFieldId { get; set; }
    }
}
