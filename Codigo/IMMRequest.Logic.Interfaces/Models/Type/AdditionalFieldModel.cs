using System.Collections.Generic;

namespace IMMRequest.Logic.Models.Type
{
    public class AdditionalFieldModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> Range { get; set; }
    }
}
