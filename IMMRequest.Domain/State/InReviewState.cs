using System;

namespace IMMRequest.Domain.State
{
    internal class InReviewState : IState
    {
        private Request Request;
        public InReviewState(Request Request)
        {
            this.Request = Request;
        }

        public void Accepted()
        {
            // The request changes the status to be accepted
            this.Request.Status = new AcceptedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Accepted");
        }

        public void Created()
        {
            // The request changes the status to be created
            this.Request.Status = new CreatedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Created");
        }

        public void Denied()
        {
            // The request changes the status to be denied
            this.Request.Status = new DeniedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Denied");
        }

        public void Done()
        {
            Console.WriteLine("ERROR: the request cannot be closed.Must complete review: Accepted / Denied");
        }

        void IState.InReview()
        {
            Console.WriteLine("ERROR: the application is already under review");
        }
    }
}
