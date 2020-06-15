namespace IMMRequest.Logic.Core.Exceptions.Request
{
    using AdditionalField;
    using Logic.Exceptions;

    public class LessAdditionalFieldsThanRequiredException : AdditionalFieldException
    {
        public LessAdditionalFieldsThanRequiredException(string errorMsg) : base(errorMsg) { }
    }
}
