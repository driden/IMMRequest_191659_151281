namespace IMMRequest.Logic.Core.Exceptions.Request
{
    using Logic.Exceptions;

    public class InvalidStateNameException : RequestException
    {
        public InvalidStateNameException(string message) : base(message) { }
    }
}
