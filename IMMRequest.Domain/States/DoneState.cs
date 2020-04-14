using IMMRequest.Domain.Exceptions;
using System;

namespace IMMRequest.Domain.States
{
    public class DoneState : State
    {
        public Request Request { get; }

        public DoneState()
        {

        }

        public DoneState(Request Request)
        {
            this.Request = Request;
        }

        public override void Accepted()
        {
            // The request changes the status to be accepted
            this.Request.Status = new AcceptedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to accepted");
        }

        public override void Created()
        {
            throw new InvalidStateException("ERROR: The request is already done. Opcions: Accepted/Denied");
        }

        public override void Denied()
        {
            // The request changes the status to be denied
            this.Request.Status = new DeniedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to denied");
        }

        public override void Done()
        {
            throw new InvalidStateException("ERROR: The request is already done");
        }

        public override void InReview()
        {
            throw new InvalidStateException("ERROR: The request is already done. Opcions are: Accepted/Denied");
        }
    }
}
