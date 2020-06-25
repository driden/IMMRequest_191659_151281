namespace IMMRequest.Logic.Core.Exceptions.AdditionalField
{
    using Logic.Exceptions;

    public class InvalidAdditionalFieldForTypeException : AdditionalFieldException
    {
        public InvalidAdditionalFieldForTypeException(string errorMessage) : base(errorMessage) { }
    }
}
