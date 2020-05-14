using System;

namespace IMMRequest.Logic.Exceptions.CreateTopic
{
    public class InvalidFieldTypeException: Exception
    {
        public InvalidFieldTypeException(string error):base(error) { }
    }
}
