using System;

namespace IMMRequest.Domain.States
{
    public class DeniedState : State
    {
        private Request Request;

        public DeniedState()
        {

        }

        public DeniedState(Request request)
        {
            this.Request = request;
        }

        public override void Accepted()
        {
            Console.WriteLine("ERROR: The request is already denied");
        }

        public override void Created()
        {
            Console.WriteLine("ERROR: The request is already denied");
        }

        public override void Denied()
        {
            Console.WriteLine("ERROR: The request is already denied");
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
            Console.WriteLine("The request " + this.Request.Id + " change to In review");
        }
    }
}
