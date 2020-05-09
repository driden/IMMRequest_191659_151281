namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    using System;

    public class NoSuchTopicException : Exception
    {
        public NoSuchTopicException(string errorMsg) : base(errorMsg) { }
    }
}
