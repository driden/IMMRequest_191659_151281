using IMMRequest.Domain.Fields;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Type
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public IEnumerable<FieldBase> AditionalFields { get; set; }
    }
}
