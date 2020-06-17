namespace IMMRequest.Logic.Core.Exceptions.Request
{
    using Logic.Exceptions;

    public class InvalidRequestIdException : RequestException
    {
        public InvalidRequestIdException(string errorMsg) : base(errorMsg) { }
    }
}
