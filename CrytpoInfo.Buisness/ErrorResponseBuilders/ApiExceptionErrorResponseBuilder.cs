using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Models;

namespace CrytpoInfo.Buisness.ErrorResponseBuilders
{
    public class ApiExceptionErrorResponseBuilder
    {
        private ApiException exception;

        public ApiExceptionErrorResponseBuilder(ApiException apiException)
        {
            this.exception = apiException;
        }

        public ErrorResponse BuildResponse()
        {
            return new ErrorResponse()
            {
                RequestId = this.exception.RequestId,
                Success = false,
                ErrorMessage = this.exception.Message,
            };
        }
    }
}
