namespace IMMRequest.Logic.Core.Exceptions.Topic
{
    using AdditionalField;
    using Logic.Exceptions;

    public class InvalidFieldTypeException : AdditionalFieldException
    {
        public InvalidFieldTypeException(string error) : base(error) { }
    }
}
