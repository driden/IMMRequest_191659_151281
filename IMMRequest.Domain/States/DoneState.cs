namespace IMMRequest.Domain.States
{
    using Exceptions;

    public class DoneState : State
    {
        public DoneState()
        {
            Description = "This request is currently done";
        }

        public DoneState(Request Request) : this()
        {
            this.Request = Request;
        }

        public override void Accepted()
        {
            Request.Status = new AcceptedState(Request);
        }

        public override void Created()
        {
            throw new InvalidStateException("Can't change a 'Done' request to 'Created', possible options are Accepted/Denied");
        }

        public override void Denied()
        {
            Request.Status = new DeniedState(Request);
        }

        public override void Done()
        {
            throw new InvalidStateException("Can't change a 'Done' request to 'Done', possible options are Accepted/Denied");
        }

        public override void InReview()
        {
            throw new InvalidStateException("Can't change a 'Done' request to 'In Review', possible options are Accepted/Denied");
        }

        public override string ToString() => "Done";
    }
}
