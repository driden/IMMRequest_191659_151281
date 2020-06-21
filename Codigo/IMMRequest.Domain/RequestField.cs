namespace IMMRequest.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RequestField
    {
        public int Id { get; set; }
        public int requestId { get; set; }
        public string Name { get; set; }
    }

    public class IntRequestField : RequestField
    {
        public List<int> Values { get; set; }

        public override string ToString() => string.Join(',', Values);
    }

    public class TextRequestField : RequestField
    {
        public List<string> Values { get; set; }

        public override string ToString() => string.Join(',', Values);
    }

    public class DateRequestField : RequestField
    {
        public List<DateTime> Values { get; set; }

        public override string ToString() => string.Join(',', Values.Select(v => v.ToString("G")));
    }

    public class BooleanRequestField : RequestField
    {
        public List<bool> Values { get; set; }
    }
}
