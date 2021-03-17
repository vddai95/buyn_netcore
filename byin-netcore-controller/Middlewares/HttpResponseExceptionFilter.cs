using byin_netcore_transver.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace byin_netcore.Middlewares
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MinValue;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception)
            {
                context.Result = new ObjectResult(new { error_type = "functional", error = exception.Error })
                {
                    StatusCode = exception.Status,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is System.Exception)
            {
                context.Result = new ObjectResult(new { error_type = "wrong exception", error = context.Exception.Message })
                {
                    StatusCode = 500,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
