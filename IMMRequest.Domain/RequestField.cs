using System;

namespace IMMRequest.Domain
{
    public class RequestField
    {
        public int Id { get; set; }
        public int requestId { get; set; }
        public string Name { get; set; }
    }

    public class IntRequestField: RequestField
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class TextRequestField: RequestField
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public class DateRequestField: RequestField
    {
        public DateTime Value { get; set; }

        public override string ToString()
        {
            return Value.ToString("G");
        }
    }
}
