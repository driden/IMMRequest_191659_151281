namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string error) : base(error) { }
    }
}
