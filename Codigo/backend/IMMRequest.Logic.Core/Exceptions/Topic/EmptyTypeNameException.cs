namespace IMMRequest.Logic.Core.Exceptions.Topic
{
    using Logic.Exceptions;

    public class EmptyTypeNameException : TypeException
    {
        public EmptyTypeNameException(string error) : base(error) { }
    }
}
