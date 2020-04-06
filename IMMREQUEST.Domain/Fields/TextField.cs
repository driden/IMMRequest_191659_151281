using System;

namespace IMMRequest.Domain.Fields
{
    public class TextField : FieldOfType<string>
    {
        public TextField()
        {
            this.FieldType = FieldType.Text;
        }

        public override void ValidateRange()
        {
            throw new NotImplementedException();
        }
    }
}
