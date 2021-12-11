using System;
using System.Net;

namespace CrytpoInfo.Buisness.Exceptions
{
    public class ClientException : ApiException
    {
        public ClientException(int errorCode, string errorDescription, Guid requestId)
            : base(HttpStatusCode.BadRequest, errorCode, errorDescription, requestId)
        {
        }
    }
}
