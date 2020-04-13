using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Domain.States
{
    public class AcceptedState : State
    {
        private Request Request;

        public AcceptedState()
        {

        }

        public AcceptedState(Request Request)
        {
            this.Request = Request;
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
            this.Request.Status = new DoneState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Done");
        }

        public override void InReview()
        {
            // The request changes the status to be in review
            this.Request.Status = new InReviewState(this.Request);
            throw new InvalidStateException("The request " + this.Request.Id + " change to In review");
        }
    }
}
