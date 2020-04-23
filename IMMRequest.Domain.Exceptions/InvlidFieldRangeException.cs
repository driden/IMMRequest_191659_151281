using System;
using System.Runtime.Serialization;

namespace IMMRequest.Domain.Exceptions
{
    [Serializable]
    public class InvalidFieldRangeException : Exception
    {
        public InvalidFieldRangeException()
        {
        }

        public InvalidFieldRangeException(string message) : base(message)
        {
        }

        public InvalidFieldRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFieldRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
