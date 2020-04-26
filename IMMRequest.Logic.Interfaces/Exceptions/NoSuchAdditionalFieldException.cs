namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class NoSuchAdditionalFieldException : Exception
    {
        public NoSuchAdditionalFieldException(string message) : base(message)
        {
        }
    }
}
