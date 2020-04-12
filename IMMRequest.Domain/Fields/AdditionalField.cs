using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
  public enum FieldType { Integer, Text, Date }
  public abstract class AdditionalField
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public FieldType FieldType { get; set; }
    public bool IsRequired { get; set; }
    public int TypeId { get; set; }

    public abstract void ValidateRange();

    public abstract void AddToRange(IItem item);
  }
}