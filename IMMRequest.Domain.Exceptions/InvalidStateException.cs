namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidStateException : Exception
    {
        public int RequestId { get; }

        public InvalidStateException() { }

        public InvalidStateException(string message)
            : base(message) { }

        public InvalidStateException(string message, Exception inner)
            : base(message, inner) { }

        public InvalidStateException(string message, int requestId)
            : this(message)
        {
            RequestId = requestId;
        }
    }
}
