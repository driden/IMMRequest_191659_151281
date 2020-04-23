using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() { }

        public InvalidEmailException(string message)
            : base(message) { }

        public InvalidEmailException(string message, Exception inner)
            : base(message, inner) { }
    }
}
