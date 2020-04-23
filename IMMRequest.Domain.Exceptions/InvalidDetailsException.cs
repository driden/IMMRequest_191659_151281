using System;

namespace IMMRequest.Domain.Exceptions
{
    public class InvalidDetailsException : Exception
    {

        public InvalidDetailsException() { }

        public InvalidDetailsException(string message)
            : base(message) { }

        public InvalidDetailsException(string message, Exception inner)
            : base(message, inner) { }

    }
}
