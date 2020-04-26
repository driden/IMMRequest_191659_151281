namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class InvalidAdditionalFieldForTypeException : Exception
    {
        public InvalidAdditionalFieldForTypeException(string message) : base(message)
        {
        }
    }
}
