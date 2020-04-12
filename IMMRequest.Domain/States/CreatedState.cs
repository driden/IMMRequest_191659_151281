using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain.States
{
    public class CreatedState : State
    {
        private Request Request;
        public CreatedState()
        {

        }
        public CreatedState(Request Request)
        {
            this.Request = Request;
        }
        public override void Accepted()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public override void Created()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public override void Denied()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public override void Done()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public override void InReview()
        {
            // The request changes the status to be reviewed
            this.Request.Status = new InReviewState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to In Review");
        }
    }
}
