using System;

namespace IMMRequest.Domain.States
{
    public class DoneState : State
    {
        private Request Request;

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
            Console.WriteLine("ERROR: The request is already done. Opcions: Accepted/Denied");
        }

        public override void Denied()
        {
            // The request changes the status to be denied
            this.Request.Status = new DeniedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to denied");
        }

        public override void Done()
        {
            Console.WriteLine("ERROR: The request is already done");
        }

        public override void InReview()
        {
            Console.WriteLine("ERROR: The request is already done. Opcions are: Accepted/Denied");
        }
    }
}
