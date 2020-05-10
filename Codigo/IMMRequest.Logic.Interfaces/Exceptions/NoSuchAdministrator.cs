using System;

namespace IMMRequest.Logic.Exceptions
{
    public class NoSuchAdministrator : Exception
    {
        public NoSuchAdministrator(string errorMessage) : base(errorMessage) { }
    }
}
