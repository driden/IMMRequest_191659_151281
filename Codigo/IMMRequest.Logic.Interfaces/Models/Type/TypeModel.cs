using System.Collections.Generic;

namespace IMMRequest.Logic.Models.Type
{
    public class TypeModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public IList<AdditionalFieldModel> AdditionalFields { get; set; }
        public bool IsActive { get; set; }

    }
}