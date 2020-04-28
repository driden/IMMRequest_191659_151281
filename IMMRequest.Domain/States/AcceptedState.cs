namespace IMMRequest.Domain.States
{
    using Exceptions;

    public class AcceptedState : State
    {
        public virtual Request Request { get; set; }

        public AcceptedState()
        {
            Description = "This request has been accepted";
        }

        public AcceptedState(Request request) : this()
        {
            Request = request;
        }
        public override void Accepted()
        {
            throw new InvalidStateException("can't update an Accepted request to 'Accepted', possible options are: Done/InReview");
        }

        public override void Created()
        {
            throw new InvalidStateException("can't update an Accepted request to 'Accepted', possible options are: Done/InReview");
        }

        public override void Denied()
        {
            throw new InvalidStateException("can't update an Accepted request to 'Denied', possible options are: Done/InReview");
        }

        public override void Done()
        {
            Request.Status = new DoneState(Request);
        }

        public override void InReview()
        {
            Request.Status = new InReviewState(Request);
        }

        public override string ToString() => "Accepted";
    }
}
