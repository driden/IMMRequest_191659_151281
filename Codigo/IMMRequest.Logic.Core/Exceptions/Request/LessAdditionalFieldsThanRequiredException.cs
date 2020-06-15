namespace IMMRequest.Logic.Core.Exceptions.Request
{
    using Logic.Exceptions;

    public class LessAdditionalFieldsThanRequiredException : AdditionalFieldException
    {
        public LessAdditionalFieldsThanRequiredException(string errorMsg) : base(errorMsg) { }
    }
}
