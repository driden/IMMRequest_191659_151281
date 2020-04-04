namespace IMMRequest.Domain.Fields
{
    public class Item<T>
    {
        public int Id { get; set; }
        //public int FieldId { get; set; }
        public T Value { get; set; }
    }
}
