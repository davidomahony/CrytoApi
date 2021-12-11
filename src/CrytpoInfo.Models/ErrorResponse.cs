using System.Net;

namespace CrytpoInfo.Models
{
    public class ErrorResponse : BaseResponse
    {
        public string ErrorMessage { get; set; } = null;

        public HttpStatusCode StatusCode { get; set; }
    }
}
