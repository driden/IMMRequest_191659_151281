namespace IMMRequest.Logic.Models.Type
{
    using System.Collections.Generic;
    using Request;

    public class CreateTypeRequest
    {
        public string Name { get; set; }
        public int TopicId { get; set; }
        public virtual IList<AdditionalFieldModel> AdditionalFields { get; set; } = new List<AdditionalFieldModel>();
    }
}
