namespace byin_netcore_transver.Exception
{
    public class HttpRegistrationException : HttpResponseException
    {
        public HttpRegistrationException(string errorMessage)
        {
            this.Status = 400;
            this.Error = new ErrorModel
            {
                Title = "registration error",
                Detail = errorMessage,
            };
        }
    }
}
