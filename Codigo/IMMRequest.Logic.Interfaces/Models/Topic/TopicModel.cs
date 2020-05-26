using System.Collections.Generic;

namespace IMMRequest.Logic.Models.Topic
{
    public class TopicModel
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public List<int> Types { get; set; }
    }
}
