using System;

namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    public class EmptyTypeNameException : Exception
    {
        public EmptyTypeNameException(string error) : base(error) { }
    }
}
