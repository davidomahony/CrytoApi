using System;

namespace CrytpoInfo.Models
{
    public class BaseResponse
    {
        public Guid RequestId { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; } = null;
    }
}
