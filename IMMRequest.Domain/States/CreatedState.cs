namespace IMMRequest.Domain.States
{
    using System;
    using Exceptions;

    public class CreatedState : State
    {
        public Request Request { get; }

        public CreatedState()
        {
            Description = "This request has been created";

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
            Request.Status = new InReviewState(Request);
            Console.WriteLine("The request " + Request.Id + " change to In Review");
        }
    }
}
