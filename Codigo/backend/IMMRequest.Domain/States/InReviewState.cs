namespace IMMRequest.Domain.States
{
    using Exceptions;

    public class InReviewState : State
    {
        public InReviewState()
        {
            Description = "In review";
        }

        public InReviewState(Request Request) : this()
        {
            this.Request = Request;
        }

        public override void Accepted()
        {
            Request.Status = new AcceptedState(Request);
        }

        public override void Created()
        {
            Request.Status = new CreatedState(Request);
        }

        public override void Denied()
        {
            Request.Status = new DeniedState(Request);
        }

        public override void Done()
        {
            throw new InvalidStateException("can't update an InReview request to 'Done', possible options are: Accepted/Created/Denied");
        }

        public override void InReview()
        {
            throw new InvalidStateException("can't update an InReview request to 'Accepted', possible options are: Accepted/Created/Denied");
        }

        public override string ToString() => "InReview";
    }
}
