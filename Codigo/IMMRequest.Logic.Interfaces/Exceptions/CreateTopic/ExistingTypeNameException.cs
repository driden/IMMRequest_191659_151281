namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    using System;

    public class ExistingTypeNameException : Exception
    {
        public ExistingTypeNameException(string name) : base(name) { }
    }
}
