using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain.State
{
    internal class CreatedState : IState
    {
        private Request Request;

        public CreatedState(Request Request)
        {
            this.Request = Request;
        }
        public void Accepted()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public void Created()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public void Denied()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public void Done()
        {
            Console.WriteLine("ERROR: The request is not created");
        }

        public void InReview()
        {
            // The request changes the status to be reviewed
            this.Request.Status = new InReviewState(this.Request);
            Console.WriteLine("The request " + this.Request.Id + " change to In Review");
        }
    }
}
