namespace IMMRequest.Domain.Exceptions
{
    using System;

    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException(string message)
            : this(message, null) { }

        public InvalidPhoneNumberException(string message, Exception inner)
            : base(message, inner) { }
    }
}
