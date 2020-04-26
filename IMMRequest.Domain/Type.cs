namespace IMMRequest.Domain
{
    using System.Collections.Generic;
    using Fields;

    public class Type
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public virtual IList<AdditionalField> AdditionalFields { get; set; }
    }
}
