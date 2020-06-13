namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidNameFormatException : Exception
    {
        public InvalidNameFormatException(string message)
            : this(message, null) { }

        public InvalidNameFormatException(string message, Exception inner)
            : base(message, inner) { }
    }
}
