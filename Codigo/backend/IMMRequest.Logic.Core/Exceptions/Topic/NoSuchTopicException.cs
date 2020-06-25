namespace IMMRequest.Logic.Core.Exceptions.Topic
{
    using Logic.Exceptions;

    public class NoSuchTopicException : TopicException
    {
        public NoSuchTopicException(string errorMsg) : base(errorMsg) { }
    }
}
