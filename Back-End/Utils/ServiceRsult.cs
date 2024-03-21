using System.Text.Json;

namespace Back_End.Utils
{
    public class ServiceResult<T>
    {
        public int StatusCode { get;private set; }
        private T? Data { get; set; }
        public string? Message { get; private set; }

        public static ServiceResult<T> SuccessResult(T? data = default, string message = "", int statusCode = 200)
        {
            return new ServiceResult<T> { StatusCode = statusCode, Data = data, Message = message };
        }

        public static ServiceResult<object> FailureResult(int statusCode, string message, object? errors = null)
        {
            return new ServiceResult<object> { StatusCode = statusCode, Message = message, Data = errors };
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return JsonSerializer.Serialize(this, options);
        }

        public T GetResult()
        {
            return Data;
        }
    }
}
