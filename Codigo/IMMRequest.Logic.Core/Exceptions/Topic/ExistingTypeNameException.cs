namespace IMMRequest.Logic.Core.Exceptions.Topic
{
    using Logic.Exceptions;

    public class ExistingTypeNameException : TypeException
    {
        public ExistingTypeNameException(string name) : base(name) { }
    }
}
