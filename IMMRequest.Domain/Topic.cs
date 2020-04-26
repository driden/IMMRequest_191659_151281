namespace IMMRequest.Domain
{
    using System.Collections.Generic;

    public class Topic
  {
    public int Id { get; set; }
    public int AreaId { get; set; }
    public string Name { get; set; }
    public virtual List<Type> Types { get; set; }
  }
}
