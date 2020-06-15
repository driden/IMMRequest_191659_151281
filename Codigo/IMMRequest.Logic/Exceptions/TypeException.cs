namespace IMMRequest.Logic.Exceptions
{
    using System;

    public abstract class TypeException : Exception
    {
        protected TypeException(string error): base(error) { }
    }
}
