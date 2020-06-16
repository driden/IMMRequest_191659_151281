namespace IMMRequest.Logic.Core.Exceptions.Topic
{
    using Logic.Exceptions;

    public class InvalidTopicIdException : TopicException
    {
        public InvalidTopicIdException(string message) : base(message) { }
    }
}
