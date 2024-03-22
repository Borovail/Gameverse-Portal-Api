using System.Text.Json;

namespace Back_End.Utils
{
    public class ServiceResult
    {
        private int statusCode;

        public int StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        private object? data;

        public object? Data
        {
            get { return data; }
            set { data = value; }
        }


        public static ServiceResult SuccessResult<T>(T? data = default, int statusCode = 200)
        {
            return new ServiceResult { Data = data,StatusCode = statusCode };
        }

        public static ServiceResult FailureResult<T>(T? errors = default, int statusCode = 400)
        {
            return new ServiceResult { Data = errors,StatusCode = statusCode };
        }
    }
}
