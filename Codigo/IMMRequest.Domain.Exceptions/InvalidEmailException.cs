namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message)
            : this(message,null) { }

        public InvalidEmailException(string message, Exception inner)
            : base(message, inner) { }
    }
}
