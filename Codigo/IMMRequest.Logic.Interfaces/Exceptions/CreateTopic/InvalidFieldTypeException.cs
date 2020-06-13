namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    using System;

    public class InvalidFieldTypeException : Exception
    {
        public InvalidFieldTypeException(string error) : base(error) { }
    }
}
