using System;

namespace IMMRequest.Logic.Exceptions
{
    public class InvalidGetRequestStatusException : Exception
    {
        public InvalidGetRequestStatusException(string message) : base(message) { }
    }
}
