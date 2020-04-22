using System;

namespace IMMRequest.Logic.Exceptions
{
    public class NoSuchTopicException : Exception
    {
        public NoSuchTopicException() { }

        public NoSuchTopicException(string message) : base(message) { }
    }
}
