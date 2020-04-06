using System;

namespace IMMRequest.Domain.Fields
{
    public class DateField : FieldOfType<DateTime>
    {
        public DateField()
        {
            this.FieldType = FieldType.Date;
        }
        public override void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
