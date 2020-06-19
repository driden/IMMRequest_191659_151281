using System;

namespace IMMRequest.Logic.Exceptions
{
    public class InvalidMailFormatException: Exception
    {
        public InvalidMailFormatException(string message): base(message) { }
    }
}
