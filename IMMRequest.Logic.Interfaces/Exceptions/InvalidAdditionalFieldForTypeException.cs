using System;
using System.Runtime.Serialization;

namespace IMMRequest.Logic.Exceptions
{
    [Serializable]
    public class InvalidAdditionalFieldForTypeException : Exception
    {
        public InvalidAdditionalFieldForTypeException()
        {
        }

        public InvalidAdditionalFieldForTypeException(string message) : base(message)
        {
        }

        public InvalidAdditionalFieldForTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidAdditionalFieldForTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
