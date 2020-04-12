using System;

namespace IMMRequest.Domain.States
{
    public class InReviewState : State
    {
        private Request Request;

        public InReviewState()
        {

        }

        public InReviewState(Request Request)
        {
            this.Request = Request;
        }

        public override void Accepted()
        {
            // The request changes the status to be accepted
            this.Request.Status = new AcceptedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Accepted");
        }

        public override void Created()
        {
            // The request changes the status to be created
            this.Request.Status = new CreatedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Created");
        }

        public override void Denied()
        {
            // The request changes the status to be denied
            this.Request.Status = new DeniedState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Denied");
        }

        public override void Done()
        {
            Console.WriteLine("ERROR: the request cannot be closed.Must complete review: Accepted / Denied");
        }

        public override void InReview()
        {
            Console.WriteLine("ERROR: the application is already under review");
        }
    }
}