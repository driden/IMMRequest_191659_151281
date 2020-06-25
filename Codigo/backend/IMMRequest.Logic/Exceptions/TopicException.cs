namespace IMMRequest.Logic.Exceptions
{
    using System;

    public abstract class TopicException : Exception
    {
        protected TopicException(string errorMsg) : base(errorMsg) { }
    }
}
