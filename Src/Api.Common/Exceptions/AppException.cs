using System;

namespace Api.Common.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message, ApiResultStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiResultStatusCode StatusCode { get; set; }
    }
}
