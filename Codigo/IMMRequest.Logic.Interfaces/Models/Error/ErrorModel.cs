namespace IMMRequest.Logic.Models.Error
{
    public class ErrorModel
    {
        public ErrorModel(string error)
        {
            Error = error;
        }

        public string Error { get; set; }
    }
}
