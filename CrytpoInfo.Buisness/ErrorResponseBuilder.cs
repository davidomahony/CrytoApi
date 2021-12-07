using CrytpoInfo.Buisness.Exceptions;
using CrytpoInfo.Models;

namespace CrytpoInfo.Buisness
{
    public class ErrorResponseBuilder
    {
        private ApiException exception;

        public ErrorResponseBuilder(ApiException apiException)
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
