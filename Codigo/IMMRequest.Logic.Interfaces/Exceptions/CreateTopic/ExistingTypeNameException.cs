using System;

namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    public class ExistingTypeNameException : Exception
    {
        public ExistingTypeNameException(string name) : base(name) { }
    }
}
