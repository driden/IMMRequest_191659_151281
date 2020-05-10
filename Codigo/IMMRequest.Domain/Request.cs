namespace IMMRequest.Domain
{
    using System.Collections.Generic;
    using Exceptions;
    using States;

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
                if (value != null && (value.Length < 2001))
                {
                    _details = value;
                }
                else if (value is null)
                {
                    throw new InvalidDetailsException("Please input some details.");
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
