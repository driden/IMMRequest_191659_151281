namespace IMMRequest.Logic.Models.Type
{
    using System.Collections.Generic;

    public class TypeModel
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public IList<AdditionalFieldModel> AdditionalFields { get; set; }
        public bool IsActive { get; set; }

    }
}
