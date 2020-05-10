namespace IMMRequest.Logic.Exceptions.RemoveType
{
    using System;

    public class InvalidIdException: Exception
    {
        public InvalidIdException(string message): base(message) { }
    }
}
