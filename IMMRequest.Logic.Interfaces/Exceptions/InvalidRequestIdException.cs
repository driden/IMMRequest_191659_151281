namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class InvalidRequestIdException: Exception
    {
        public InvalidRequestIdException(string message): base(message) { }
    }
}
