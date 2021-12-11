using CrytpoInfo.Buisness;
using CrytpoInfo.Buisness.ErrorResponseBuilders;
using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CrytpoInfo.CryptAPI.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger) => this.logger = logger;

        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            // As we only have one exception at the moment this will do.
            if (exception is ApiException apiException)
            {
                var response = new ApiExceptionErrorResponseBuilder(apiException).BuildResponse();
                this.logger.LogWarning($"Returning response {response.StatusCode} due to {response.ErrorMessage} with requestId {response.RequestId}");
                return response;
            }

            this.logger.LogError($"Failed to catch exception with message {exception.Message}");
            // This is weird and should not happen.
            return new ErrorResponse()
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                ErrorMessage = exception.Message
            };
        }
    }
}
