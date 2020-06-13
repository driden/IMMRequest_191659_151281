namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    using System;

    public class EmptyTypeNameException : Exception
    {
        public EmptyTypeNameException(string error) : base(error) { }
    }
}
