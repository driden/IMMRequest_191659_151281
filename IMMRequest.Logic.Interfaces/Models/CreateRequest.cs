using System;
using System.Collections.Generic;

namespace IMMRequest.Logic.Models
{
    public class CreateRequest
    {
        public string Details { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int TopicId { get; set; } = -1;
        public IEnumerable<FieldRequest> AdditionalFields { get; set; } = new List<FieldRequest>();
    }

    public class FieldRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
    }
}
