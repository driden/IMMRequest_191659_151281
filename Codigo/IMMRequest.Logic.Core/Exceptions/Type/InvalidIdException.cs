namespace IMMRequest.Logic.Core.Exceptions.Type
{
    using Logic.Exceptions;

    public class InvalidIdException: TypeException
    {
        public InvalidIdException(string message): base(message) { }
    }
}
