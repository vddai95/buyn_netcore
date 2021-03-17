namespace byin_netcore_transver.Exception
{
    public class HttpResponseException : System.Exception
    {
        public int Status { get; set; } = 500;

        public ErrorModel Error { get; set; } = new ErrorModel {Title = "Internal Server Error", Detail = "Some error have been occured, if the problem persists, please contact IT support" };
    }
}
