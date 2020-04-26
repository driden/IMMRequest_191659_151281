namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class NoSuchTopicException : Exception
    {
        public NoSuchTopicException(string message) : base(message) { }
    }
}
