using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain.Exceptions
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException() { }

        public InvalidPhoneNumberException(string message)
            : base(message) { }

        public InvalidPhoneNumberException(string message, Exception inner)
            : base(message, inner) { }
    }
}
