using IMMRequest.Domain.States;
using IMMRequest.Domain.Exceptions;
using System.Collections.Generic;

namespace IMMRequest.Domain
{
    public class Request
    {
        public int Id { get; set; }
        public virtual State Status { get; set; }
        public virtual Citizen Citizen { get; set; }
        public virtual Type Type { get; set; }
        public virtual List<RequestField> FieldValues { get; set; } = new List<RequestField>();
        private string _details;
        public string Details
        {
            get => _details;
            set
            {
                if ((value.Length < 2001))
                {
                    _details = value;
                }
                else
                {
                    throw new InvalidDetailsException("The detail is too long");
                }
            }
        }

        public Request()
        {
            Status = new CreatedState(this);
        }
    }
}
