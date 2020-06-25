namespace IMMRequest.Logic.Core.Exceptions.Request
{
    using Logic.Exceptions;

    public class NoSuchRequestException : RequestException
    {
        public NoSuchRequestException(string errorMsg) : base(errorMsg)
        {
        }
    }
}
