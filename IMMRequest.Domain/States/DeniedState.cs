namespace IMMRequest.Domain.States
{
    using Exceptions;

    public class DeniedState : State
    {
        public virtual Request Request { get; set; }

        public DeniedState()
        {
            Description = "This request has been denied";
        }

        public DeniedState(Request request) : this()
        {
            Request = request;
        }

        public override void Accepted()
        {
            throw new InvalidStateException("Can't update a Denied request to 'Accepted', possible options are: Done/InReview");
        }

        public override void Created()
        {
            throw new InvalidStateException("Can't update a Denied request to 'Accepted', possible options are: Done/InReview");
        }

        public override void Denied()
        {
            throw new InvalidStateException("Can't update a Denied request to 'Accepted', possible options are: Done/InReview");
        }

        public override void Done()
        {
            Request.Status = new DoneState(Request);
        }

        public override void InReview()
        {
            Request.Status = new InReviewState(Request);
        }

        public override string ToString() => "Denied";
    }
}
