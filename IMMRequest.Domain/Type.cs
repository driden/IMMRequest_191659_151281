using IMMRequest.Domain.Fields;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Type
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Name { get; set; }
        public virtual IList<AdditionalField> AdditionalFields { get; set; }

        public Type()
        {

        }
    }
}
