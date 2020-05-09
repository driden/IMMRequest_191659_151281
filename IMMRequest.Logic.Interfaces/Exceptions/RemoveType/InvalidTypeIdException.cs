namespace IMMRequest.Logic.Exceptions.RemoveType
{
    using System;

    public class InvalidTypeIdException: Exception
    {
        public InvalidTypeIdException(string message): base(message) { }
    }
}
