using System;

namespace IMMRequest.Domain.Exceptions
{
    public class InvalidAdminToken : Exception
    {
        public InvalidAdminToken(string errorMessage) : base(errorMessage) { }
    }
}
