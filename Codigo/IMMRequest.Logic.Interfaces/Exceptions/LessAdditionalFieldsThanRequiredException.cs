namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class LessAdditionalFieldsThanRequiredException : Exception
    {
        public LessAdditionalFieldsThanRequiredException(string message) : base(message)
        {
        }
    }
}
