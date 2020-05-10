namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class InvalidStateNameException : Exception
    {
        public InvalidStateNameException(string message) : base(message) { }
    }
}
