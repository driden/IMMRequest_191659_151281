namespace IMMRequest.Domain.Fields
{
    public class Item<T>
    {
        public int Id { get; set; }
        public T Value { get; set; }
        public int AdditionalFieldId { get; set; }
    }
}
