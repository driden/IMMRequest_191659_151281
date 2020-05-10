using System;

namespace IMMRequest.Domain
{
    using Exceptions;

    public class Admin : User
    {
        private string password;

        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidPasswordException("An admin can't have an empty password.");
                }
                password = value;
            }
        }

        public Guid Token { get; set; }
    }
}
