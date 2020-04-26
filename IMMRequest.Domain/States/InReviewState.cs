namespace IMMRequest.Domain.States
{
    using System;
    using Exceptions;

    public class InReviewState : State
    {
        public Request Request { get; }

        public InReviewState()
        {
            Description = "This request is currently in-review";
        }

        public InReviewState(Request Request) : this()
        {
            this.Request = Request;
        }

        public override void Accepted()
        {
            // The request changes the status to be accepted
            Request.Status = new AcceptedState(Request);
            Console.WriteLine("The request " + Request.Id + " change to Accepted");
        }

        public override void Created()
        {
            // The request changes the status to be created
            Request.Status = new CreatedState(Request);
            Console.WriteLine("The request " + Request.Id + " change to Created");
        }

        public override void Denied()
        {
            // The request changes the status to be denied
            Request.Status = new DeniedState(Request);
            Console.WriteLine("The request " + Request.Id + " change to Denied");
        }

        public override void Done()
        {
            throw new InvalidStateException("ERROR: the request cannot be closed.Must complete review: Accepted / Denied");
        }

        public override void InReview()
        {
            throw new InvalidStateException("ERROR: the application is already under review");
        }
    }
}
