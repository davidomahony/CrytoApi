using CrytpoInfo.Buisness;
using CrytpoInfo.Buisness.ErrorResponseBuilders;
using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CrytpoInfo.CryptAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            // As we only have one exception at the moment this will do.
            if (exception is ApiException apiException)
            {
                var responseBuilder = new ApiExceptionErrorResponseBuilder(apiException);
                return responseBuilder.BuildResponse();
            }

            // This is weird and should not happen.
            return new ErrorResponse()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message
            };
        }
    }
}
