using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Domain.States
{
    public class AcceptedState : State
    {
        public Request Request { get; }

        public AcceptedState()
        {
        }

        public AcceptedState(Request request)
        {
            this.Request = request;
        }
        public override void Accepted()
        {
            throw new InvalidStateException("ERROR: The request is already accepted");
        }

        public override void Created()
        {
            throw new InvalidStateException("ERROR: The request is already accepted");
        }

        public override void Denied()
        {
            throw new InvalidStateException("ERROR: The request is already accepted");
        }

        public override void Done()
        {
            // The request changes the status to be done
            Request.Status = new DoneState(Request);
            Console.WriteLine("The request " + Request.Id + " change to Done");
        }

        public override void InReview()
        {
            // The request changes the status to be in review
            Request.Status = new InReviewState(Request);
            Console.WriteLine("The request " + Request.Id + " change to In review");
        }
    }
}
