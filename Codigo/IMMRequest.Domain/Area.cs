namespace IMMRequest.Domain
{
    using System.Collections.Generic;

    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Topic> Topics { get; set; }

        public Area()
        {
            Topics = new List<Topic>();
        }
    }
}
