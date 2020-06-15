namespace IMMRequest.Logic.Exceptions
{
    using System;
    public abstract class AdditionalFieldException: Exception
    {
        protected AdditionalFieldException(string errorMsg):base(errorMsg) { }
    }
}
