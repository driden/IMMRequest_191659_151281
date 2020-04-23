using IMMRequest.Domain.States;
using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Domain
{
    public class Request
    {
        public int Id { get; set; }
        public virtual State Status { get; set; }
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

        public virtual Citizen Citizen { get; set; }
        public virtual Topic Topic { get; set; }

        public Request()
        {
            Status = new CreatedState(this);
        }
    }
}
