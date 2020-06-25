namespace IMMRequest.Logic.Core.Exceptions.Account
{
    using Logic.Exceptions;

    public class NoSuchAdministrator : AccountException
    {
        public NoSuchAdministrator(string errorMsg) : base(errorMsg) { }
    }
}
