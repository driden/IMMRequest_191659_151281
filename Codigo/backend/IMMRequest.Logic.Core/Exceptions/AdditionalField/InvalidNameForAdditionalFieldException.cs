namespace IMMRequest.Logic.Core.Exceptions.AdditionalField
{
    using Logic.Exceptions;

    public class InvalidNameForAdditionalFieldException : AdditionalFieldException
    {
        public InvalidNameForAdditionalFieldException(string errorMsg) : base(errorMsg) { }
    }
}
