namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class InvalidFieldValueCastForFieldTypeException : Exception
    {
        public InvalidFieldValueCastForFieldTypeException(string message) : base(message)
        {
        }
    }
}
