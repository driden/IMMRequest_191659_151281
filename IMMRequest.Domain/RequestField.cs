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
    }
    public class TextRequestField: RequestField
    {
        public string Value { get; set; }
    }
    public class DateRequestField: RequestField
    {
        public DateTime Value { get; set; }
    }
}
