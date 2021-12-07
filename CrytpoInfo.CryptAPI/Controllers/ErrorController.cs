using CrytpoInfo.Buisness;
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

            if (exception is ApiException apiException)
            {
                var responseBuilder = new ErrorResponseBuilder(apiException);
                return responseBuilder.BuildResponse();
            }

            return null;
        }
    }
}
