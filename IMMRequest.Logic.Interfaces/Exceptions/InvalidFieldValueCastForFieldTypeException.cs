using System;
using System.Runtime.Serialization;

namespace IMMRequest.Logic.Exceptions
{
    [Serializable]
    public class InvalidFieldValueCastForFieldTypeException : Exception
    {
        public InvalidFieldValueCastForFieldTypeException()
        {
        }

        public InvalidFieldValueCastForFieldTypeException(string message) : base(message)
        {
        }

        public InvalidFieldValueCastForFieldTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidFieldValueCastForFieldTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
