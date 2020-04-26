namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidFieldRangeException : Exception
    {
        public InvalidFieldRangeException(string message) : base(message)
        {
        }
    }
}
