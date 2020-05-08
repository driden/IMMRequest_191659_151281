namespace IMMRequest.Logic.Exceptions
{
    using System;

    public class NoSuchTypeException : Exception
    {
        public NoSuchTypeException(string message) : base(message) { }
    }
}
