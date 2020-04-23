using System;
using System.Runtime.Serialization;

namespace IMMRequest.Logic.Tests
{
    public class LessAdditionalFieldsThanRequiredException : Exception
    {
        public LessAdditionalFieldsThanRequiredException()
        {
        }

        public LessAdditionalFieldsThanRequiredException(string message) : base(message)
        {
        }

        public LessAdditionalFieldsThanRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LessAdditionalFieldsThanRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
