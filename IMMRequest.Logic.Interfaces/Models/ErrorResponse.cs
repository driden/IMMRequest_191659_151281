namespace IMMRequest.Logic.Models
{
    public class ErrorResponse
    {
        public ErrorResponse(string error)
        {
            Error = error;
        }

        public string Error { get; set; }
    }
}
