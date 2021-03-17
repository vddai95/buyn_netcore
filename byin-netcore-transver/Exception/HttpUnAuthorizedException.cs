namespace byin_netcore_transver.Exception
{
    public class HttpUnAuthorizedException : HttpResponseException
    {
        public HttpUnAuthorizedException()
        {
            this.Status = 403;
            this.Error = new ErrorModel
            {
                Title = "unauthorized",
                Detail = "action is unauthorized",
            };
        }
    }
}
