using IMMRequest.Domain.Exceptions;
using System;


namespace IMMRequest.Domain.States
{
    public class CreatedState : State
    {
        public Request Request { get; }

        public CreatedState()
        {
            this.Description = "This request has been created";

        }

        public CreatedState(Request Request) : this()
        {
            this.Request = Request;
        }
        public override void Accepted()
        {
            throw new InvalidStateException("ERROR: The request is not created");
        }

        public override void Created()
        {
            throw new InvalidStateException("ERROR: The request is not created");
        }

        public override void Denied()
        {
            throw new InvalidStateException("ERROR: The request is not created");
        }

        public override void Done()
        {
            throw new InvalidStateException("ERROR: The request is not created");
        }

        public override void InReview()
        {
            // The request changes the status to be reviewed
            Request.Status = new InReviewState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to In Review");
        }
    }
}
