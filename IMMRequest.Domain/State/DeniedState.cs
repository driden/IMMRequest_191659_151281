using System;

namespace IMMRequest.Domain.State
{
    internal class DeniedState : IState
    {
        private Request Request;

        public DeniedState(Request request)
        {
            this.Request = request;
        }

        public void Accepted()
        {
            Console.WriteLine("ERROR: The request is already denied");
        }

        public void Created()
        {
            Console.WriteLine("ERROR: The request is already denied");
        }

        public void Denied()
        {
            Console.WriteLine("ERROR: The request is already denied");
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
