namespace IMMRequest.Logic.Core.Exceptions.AdditionalField
{
    using Logic.Exceptions;

    public class InvalidFieldValueCastForFieldTypeException : AdditionalFieldException
    {
        public InvalidFieldValueCastForFieldTypeException(string message) : base(message) { }
    }
}
