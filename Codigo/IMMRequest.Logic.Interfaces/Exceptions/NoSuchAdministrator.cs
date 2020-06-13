namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class NoSuchAdministrator : Exception
    {
        public NoSuchAdministrator(string errorMessage) : base(errorMessage) { }
    }
}
