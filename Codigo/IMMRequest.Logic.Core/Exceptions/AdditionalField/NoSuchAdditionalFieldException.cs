namespace IMMRequest.Logic.Core.Exceptions.AdditionalField
{
    using Logic.Exceptions;

    public class NoSuchAdditionalFieldException : AdditionalFieldException
    {
        public NoSuchAdditionalFieldException(string errorMsg) : base(errorMsg)
        {
        }
    }
}
