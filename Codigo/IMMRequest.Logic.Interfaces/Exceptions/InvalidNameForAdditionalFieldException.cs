namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class InvalidNameForAdditionalFieldException : Exception
    {
        public InvalidNameForAdditionalFieldException(string msg) : base(msg) { }
    }
}
