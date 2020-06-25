namespace IMMRequest.Logic.Exceptions
{
    using System;
    
    public class InvalidDateRageException: Exception
    {
        public InvalidDateRageException(string message): base(message) { }
    }
}