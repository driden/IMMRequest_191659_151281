namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidDetailsException : Exception
    {

        public InvalidDetailsException(string message)
            : this(message, null) { }

        public InvalidDetailsException(string message, Exception inner)
            : base(message, inner) { }

    }
}
