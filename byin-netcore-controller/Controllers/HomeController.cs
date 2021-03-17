using byin_netcore_transver.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace byin_netcore.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("/ErrorLocal")]
        [AllowAnonymous]
        public IActionResult ErrorLocal() 
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if(context.Error is HttpResponseException exception)
            {
                return new ObjectResult(new { error_type = "functional", error = exception.Error })
                {
                    StatusCode = exception.Status,
                };
            }
            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }

        [AllowAnonymous]
        [Route("/ErrorProd")]
        public IActionResult ErrorProd()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context.Error is HttpResponseException exception)
            {
                return new ObjectResult(new { error_type = "functional", error = exception.Error })
                {
                    StatusCode = exception.Status,
                };
            }
            return Problem();
        }
    }
}