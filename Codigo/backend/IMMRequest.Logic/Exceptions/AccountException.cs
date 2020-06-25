namespace IMMRequest.Logic.Exceptions
{
    using System;

    public abstract class AccountException : Exception
    {
        protected AccountException(string errorMsg) : base(errorMsg) { }
    }
}
