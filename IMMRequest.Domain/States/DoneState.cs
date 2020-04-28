namespace IMMRequest.Domain.States
{
    using System;
    using Exceptions;

    public class DoneState : State
    {
        public Request Request { get; }

        public DoneState()
        {
            Description = "This request is currently done";
        }

        public DoneState(Request Request) : this()
        {
            this.Request = Request;
        }

        public override void Accepted()
        {
            // The request changes the status to be accepted
            Request.Status = new AcceptedState(Request);
            Console.WriteLine("The request " + Request.Id + " change to accepted");
        }

        public override void Created()
        {
            throw new InvalidStateException("ERROR: The request is already done. Opcions: Accepted/Denied");
        }

        public override void Denied()
        {
            // The request changes the status to be denied
            Request.Status = new DeniedState(Request);
            Console.WriteLine("The request " + Request.Id + " change to denied");
        }

        public override void Done()
        {
            throw new InvalidStateException("ERROR: The request is already done");
        }

        public override void InReview()
        {
            throw new InvalidStateException("ERROR: The request is already done. Opcions are: Accepted/Denied");
        }

        public override string ToString() => "Done";
    }
}
