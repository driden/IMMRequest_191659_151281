namespace IMMRequest.Logic.Core.Exceptions.Account
{
    using Logic.Exceptions;

    public class InvalidAdminIdException : AccountException
    {
        public InvalidAdminIdException(string errorMsg) : base(errorMsg) { }
    }
}
