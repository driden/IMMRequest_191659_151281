using System;
using System.Runtime.Serialization;

namespace IMMRequest.Logic.Exceptions
{
    public class NoSuchAdditionalFieldException : Exception
    {
        public NoSuchAdditionalFieldException()
        {
        }

        public NoSuchAdditionalFieldException(string message) : base(message)
        {
        }

        public NoSuchAdditionalFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoSuchAdditionalFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
