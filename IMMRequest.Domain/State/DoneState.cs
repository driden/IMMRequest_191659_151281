using System;

namespace IMMRequest.Domain.State
{
    internal class DoneState : IState
    {
        private Request Request;
        public DoneState(Request Request)
        {
            this.Request = Request;
        }
        public void Accepted()
        {
            // The request changes the status to be accepted
            this.Request.Status = new AcceptedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to accepted");
        }

        public void Created()
        {
            Console.WriteLine("ERROR: The request is already done. Opcions: Accepted/Denied");
        }

        public void Denied()
        {
            // The request changes the status to be denied
            this.Request.Status = new DeniedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to denied");
        }

        public void Done()
        {
            Console.WriteLine("ERROR: The request is already done");
        }

        public void InReview()
        {
            Console.WriteLine("ERROR: The request is already done. Opcions are: Accepted/Denied");
        }
    }
}
