namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidStateException : Exception
    { 
        public InvalidStateException(string message)
            : this(message, null) { }

        public InvalidStateException(string message, Exception inner)
            : base(message, inner) { }
    }
}
