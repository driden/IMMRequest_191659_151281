namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidDetailsException : Exception
    {

        public InvalidDetailsException() { }

        public InvalidDetailsException(string message)
            : base(message) { }

        public InvalidDetailsException(string message, Exception inner)
            : base(message, inner) { }

    }
}
