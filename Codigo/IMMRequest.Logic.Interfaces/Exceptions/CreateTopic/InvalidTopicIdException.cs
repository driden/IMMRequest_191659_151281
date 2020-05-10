namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    using System;

    public class InvalidTopicIdException: Exception
    {
        public InvalidTopicIdException(string message): base(message) { }
    }
}
