namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class InvalidGetRequestStatusException : Exception
    {
        public InvalidGetRequestStatusException(string message) : base(message) { }
    }
}
