namespace IMMRequest.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } /* Unique */
        public string PhoneNumber { get; set; }
        // Need to have an association with the request made
        public int RequestId { get; set; }

        public User()
        {

        }
    }
}
