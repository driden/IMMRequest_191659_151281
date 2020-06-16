namespace IMMRequest.Logic.Models.Area
{
    using System.Collections.Generic;

    public class AreaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> Topics { get; set; }
    }
}
