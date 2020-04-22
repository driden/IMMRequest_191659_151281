using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
  public class IntegerField : AdditionalField
  {
    public virtual IEnumerable<IntegerItem> Range { get; set; } = new List<IntegerItem>();
    public int Value { get; set; } = default(int);

    public void AddToRange(int item)
    {
      (this.Range as IList<IntegerItem>).Add(new IntegerItem { Value = item });
    }

    public IntegerField()
    {
      this.FieldType = FieldType.Integer;
    }

    public override void ValidateRange()
    {
      throw new NotImplementedException();
    }

    public override void AddToRange(IItem item)
    {
      if (item.Type == FieldType.Integer)
      {
        this.AddToRange(((IntegerItem)item).Value);
      }
    }
  }
}
