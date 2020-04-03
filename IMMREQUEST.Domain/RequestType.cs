using System;

namespace IMMRequest.Domain
{
    public class RequestType
    {
        public int Id { get; set; }
        public string Topic { get; set; }

        public Field[] AditionalFields { get; set; }

        public RequestType()
        {
            
        }
    }
}