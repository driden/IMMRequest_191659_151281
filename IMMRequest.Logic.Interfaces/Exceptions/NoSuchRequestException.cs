using System;
using System.Threading;

namespace IMMRequest.Logic.Exceptions
{
    public class NoSuchRequestException : Exception
    {
        public NoSuchRequestException(string errorMsg) : base(errorMsg)
        {
        }
    }
}
