namespace API.Helper
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {

        }
        public ErrorResponse(int status, string message)
        {
            this.status = status;
            this.message = message;
        }
        public int status { get; set; }
        public string message { get; set; }
    }
}
