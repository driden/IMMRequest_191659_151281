using System;

namespace IMMRequest.Domain
{
    public class Citizen : User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } /* Unique */ 
        public string PhoneNumber { get; set; }

        public Citizen()
        {

        }
    }
}