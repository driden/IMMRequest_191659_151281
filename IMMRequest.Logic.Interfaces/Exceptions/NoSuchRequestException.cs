namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class NoSuchRequestException : Exception
    {
        public NoSuchRequestException(string errorMsg) : base(errorMsg)
        {
        }
    }
}
