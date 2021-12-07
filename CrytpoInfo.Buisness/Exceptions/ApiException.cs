using System;
using System.Net;

namespace CrytpoInfo.Buisness.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public Guid RequestId { get; }

        public ApiException(HttpStatusCode httpStatusCode, int errorCode, string errorDescription, Guid requestId)
            : base($"{errorCode}-{errorDescription}")
        {
            this.StatusCode = httpStatusCode;
            this.RequestId = requestId;
        }
    }
}
