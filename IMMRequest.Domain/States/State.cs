using System;
using System.Collections.Generic;
using System.Text;

namespace IMMRequest.Domain.States
{
    public abstract class State
    {
        public int Id { get; set; }

        public abstract void Created();

        public abstract void InReview();

        public abstract void Accepted();

        public abstract void Denied();

        public abstract void Done();
    }
}
