namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidStateException : Exception
    {
        public int RequestId { get; }

        public InvalidStateException(string message)
            : this(message, null) { }

        public InvalidStateException(string message, Exception inner)
            : base(message, inner) { }

        public InvalidStateException(string message, int requestId)
            : this(message, null)
        {
            RequestId = requestId;
        }
    }
}
