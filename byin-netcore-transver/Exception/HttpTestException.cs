namespace byin_netcore_transver.Exception
{
    public class HttpTestException : HttpResponseException
    {
        public HttpTestException()
        {
            this.Status = 500;
            this.Error = new ErrorModel
            {
                Title = "test exception",
                Detail = "this is just a test exception",
            };
        }
    }
}
