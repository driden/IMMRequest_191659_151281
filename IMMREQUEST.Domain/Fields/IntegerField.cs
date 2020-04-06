using System;

namespace IMMRequest.Domain.Fields
{
    public class IntegerField : FieldOfType<int>
    {
        public IntegerField()
        {
            this.FieldType = FieldType.Integer;
        }

        public override void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
