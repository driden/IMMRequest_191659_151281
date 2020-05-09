using System.Collections.Generic;

namespace IMMRequest.Logic.Models
{
    public class CreateTypeRequest
    {
        public string Name { get; set; }
        public int TopicId { get; set; }
        public virtual IList<NewTypeAdditionalField> AdditionalFields { get; set; } = new List<NewTypeAdditionalField>();
    }

    public class NewTypeAdditionalField
    {
        public string Name { get; set; }
        public string FieldType { get; set; }
        public bool IsRequired { get; set; }
        
        public IEnumerable<FieldRequestModel> Range { get; set; }
    }
}