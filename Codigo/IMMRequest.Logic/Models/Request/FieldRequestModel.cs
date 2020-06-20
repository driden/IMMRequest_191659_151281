namespace IMMRequest.Logic.Models.Request
{
    public class FieldRequestModel
    {
        public string Name { get; set; }
        public string Values { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            var other = (FieldRequestModel) obj;
            return Equals(other);
        }

        protected bool Equals(FieldRequestModel other)
        {
            return Name == other.Name && Values == other.Values;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Values != null ? Values.GetHashCode() : 0);
            }
        }
    }
}
