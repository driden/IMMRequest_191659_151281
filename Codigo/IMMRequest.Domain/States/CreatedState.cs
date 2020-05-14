namespace IMMRequest.Domain.States
{
    using Exceptions;

    public class CreatedState : State
    {
        public CreatedState()
        {
            Description = "This request has been created";
        }

        public CreatedState(Request Request) : this()
        {
            this.Request = Request;
        }
        public override void Accepted()
        {
            throw new InvalidStateException("can't update a created request to 'Accepted', possible options are: InReview");
        }

        public override void Created()
        {
            throw new InvalidStateException("can't update a created request to 'Created', possible options are: InReview");
        }

        public override void Denied()
        {
            throw new InvalidStateException("can't update a created request to 'Denied', possible options are: InReview");
        }

        public override void Done()
        {
            throw new InvalidStateException("can't update a created request to 'Done', possible options are: InReview");
        }

        public override void InReview()
        {
            Request.Status = new InReviewState(Request);
        }

        public override string ToString() => "Created";
    }
}
