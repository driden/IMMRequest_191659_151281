using System;

namespace IMMRequest.Domain.State
{
    internal class AcceptedState : IState
    {
        private Request Request;
        public AcceptedState(Request Request)
        {
            this.Request = Request;
        }
        public void Accepted()
        {
            Console.WriteLine("ERROR: The request is already accepted");
        }

        public void Created()
        {
            Console.WriteLine("ERROR: The request is already accepted");
        }

        public void Denied()
        {
            Console.WriteLine("ERROR: The request is already accepted");
        }

        public void Done()
        {
            // The request changes the status to be done
            this.Request.Status = new DoneState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to Done");
        }

        public void InReview()
        {
            // The request changes the status to be in review
            this.Request.Status = new InReviewState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to In review");
        }
    }
}
