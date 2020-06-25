namespace IMMRequest.Logic.Core.Exceptions.Type
{
    using Logic.Exceptions;

    public class NoSuchTypeException : TypeException
    {
        public NoSuchTypeException(string errorMsg) : base(errorMsg) { }
    }
}
