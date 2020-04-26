
namespace IMMRequest.Domain.States
{
    public abstract class State
    {
        public int RequestId { get; set; }

        public int Id { get; set; }

        public string Description { get; protected set; }

        public abstract void Created();

        public abstract void InReview();

        public abstract void Accepted();

        public abstract void Denied();

        public abstract void Done();
    }
}
