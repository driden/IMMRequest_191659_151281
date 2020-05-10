namespace IMMRequest.Logic.Models
{
    using System.Collections.Generic;

    public class CreateRequest
    {
        public string Details { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int TypeId { get; set; } = -1;
        public IEnumerable<FieldRequestModel> AdditionalFields { get; set; } = new List<FieldRequestModel>();
    }
}
