namespace IMMRequest.Logic.Models
{
    public class FieldRequestModel
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is FieldRequestModel frm && frm.Name == Name && frm.Value == Value;
        }
    }
}
