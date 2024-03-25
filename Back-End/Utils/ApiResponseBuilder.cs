using Back_End.Models.ResponseModels;

namespace Back_End.Utils
{
    //вообще  было бы довольно  логично сделать  публичное  поле  статус  код  билдеру  удалить  класы  респонс и сервис  респонс
    //    и  с сервиса возвращать билдер и в контроллере   получать через  билдера  статус  код  и  билдть  респонс
    //    и можно переименовать дата  в респонс  что бы  было  response.StatusCode  и response.Build()  наглядно и удобно


    public class ApiResponseBuilder
    {
        public int StatusCode = 200;
        private readonly Dictionary<string, object> _data = [];
        private List<string> _errors = [];

        public ApiResponseBuilder SetStatusCode(int statusCode)
        {
            StatusCode = statusCode;
            return this;
        }
        public ApiResponseBuilder SetErrors(List<string> errors)
        {
            _errors = errors;
            return this;
        }

        public ApiResponseBuilder AddData(string key, object value)
        {
            _data[key] = value;
            return this;
        }

        public ApiResponseBuilder SuccessResponse(int statusCode=200)
        {
            return new ApiResponseBuilder().SetStatusCode(statusCode)
                .AddData("message", "Success");
        }

        public ApiResponseBuilder FailureResponse(int statusCode=400)
        {
            return new ApiResponseBuilder().SetStatusCode(statusCode)
                .AddData("message", "Error");
        }



        
        public ApiResponseModel Build()
        {
            return new ApiResponseModel(_data, _errors);
        }
    }
}
