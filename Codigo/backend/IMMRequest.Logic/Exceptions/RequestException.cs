namespace IMMRequest.Logic.Exceptions
{
    using System;

    public abstract class RequestException : Exception
    {
        protected RequestException(string errorMsg) : base(errorMsg) { }
    }
}
