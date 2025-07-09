using System.Net;

namespace WebApiProj1.Models
{
    public class GenericRes<T>
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode HttpStatus { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static GenericRes<T> Success(T data, string message = "Success")
        {
            return new GenericRes<T>
            {
                IsSuccess = true,
                HttpStatus = HttpStatusCode.OK,
                Message = message,
                Data = data
            };
        }

        public static GenericRes<T> Failed(T data, string message = "An error occurred")
        {
            return new GenericRes<T>
            {
                IsSuccess = false,
                HttpStatus = HttpStatusCode.BadRequest,
                Message = message,
                Data = data
            };
        }

    }
}
