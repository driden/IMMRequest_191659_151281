using System;

namespace IMMRequest.Domain.Exceptions
{
    public class InvalidPasswordException: Exception
    {
        public InvalidPasswordException(string error): base(error) { }
    }
}
