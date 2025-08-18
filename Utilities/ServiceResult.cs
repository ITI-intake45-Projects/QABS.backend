using System.Net;

namespace Utilities
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static ServiceResult SuccessResult(string message = "Success",HttpStatusCode statuscode = HttpStatusCode.OK) =>
            new() 
            { 
                IsSuccess = true, 
                Message = message, 
                StatusCode = statuscode };


        public static ServiceResult FailureResult(string message = "An error occurred.", HttpStatusCode statuscode = HttpStatusCode.BadRequest) =>
            new()
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statuscode

            };
    }


    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> SuccessResult(T data, string message = "Success",
            HttpStatusCode statusCode = HttpStatusCode.OK) =>
            new()
            {
                IsSuccess = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };

        public static ServiceResult<T> FailureResult(string message = "An error occurred.",
            HttpStatusCode statusCode = HttpStatusCode.BadRequest) =>
            new()
            {
                IsSuccess = false,
                Message = message,
                StatusCode = statusCode,
                Data = default
            };
    }

}
