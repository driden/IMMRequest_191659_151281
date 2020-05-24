using System;

namespace IMMRequest.Logic.Exceptions
{
    public class InvalidNameForAdditionalFieldException : Exception
    {
        public InvalidNameForAdditionalFieldException(string msg) : base(msg) { }
    }
}
