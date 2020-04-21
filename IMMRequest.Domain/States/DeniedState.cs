using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Domain.States
{
    public class DeniedState : State
    {
        public Request Request { get; }

        public DeniedState()
        {
            this.Description = "This request has been denied";
        }

        public DeniedState(Request request) : this()
        {
            this.Request = request;
        }

        public override void Accepted()
        {
            throw new InvalidStateException("ERROR: The request is already denied");
        }

        public override void Created()
        {
            throw new InvalidStateException("ERROR: The request is already denied");
        }

        public override void Denied()
        {
            throw new InvalidStateException("ERROR: The request is already denied");
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
