namespace IMMRequest.Logic.Models
{
    public class CreateRequest
    {
        public string Details { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int TopicId { get; set; } = -1;
    }
}
