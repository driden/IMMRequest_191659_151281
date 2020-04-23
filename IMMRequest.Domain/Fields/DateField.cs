using System;
using System.Collections.Generic;

namespace IMMRequest.Domain.Fields
{
  public class DateField : AdditionalField
  {
    public virtual IEnumerable<DateItem> Range { get; set; } = new List<DateItem>();

    public void AddToRange(DateTime item)
    {
      (this.Range as IList<DateItem>).Add(new DateItem { Value = item });
    }

    public DateField()
    {
      this.FieldType = FieldType.Date;
    }

    public override void ValidateRange(object value)
    {
      throw new NotImplementedException();
    }

    public override void AddToRange(IItem item)
    {
      if (item.Type == FieldType.Integer)
      {
        this.AddToRange(((DateItem)item).Value);
      }
    }

  }
}
