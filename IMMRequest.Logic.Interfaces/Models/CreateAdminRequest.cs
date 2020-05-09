using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Logic.Models
{
    public class CreateAdminRequest
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
