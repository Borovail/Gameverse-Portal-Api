namespace Back_End.Utils
{

    //вообще  было бы довольно  логично сделать  публичное  поле  статус  код  билдеру  удалить  класы  респонс и сервис  респонс
    //    и  с сервиса возвращать билдер и в контроллере   получать через  билдера  статус  код  и  билдть  респонс
    //    и можно переименовать дата  в респонс  что бы  было  response.StatusCode  и response.Build()  наглядно и удобно


    public class ApiResponseBuilder
    {
        private int _statusCode = 200;
        private readonly Dictionary<string, object> _data = [];
        private List<string> _errors = [];

        public ApiResponseBuilder SetStatusCode(int statusCode)
        {
            _statusCode = statusCode;
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


        public ServiceResponse Build()
        {
            return new ServiceResponse(_statusCode, _data, _errors);
        }
    }

    public class ApiResponse : Response
    {

        public ApiResponse(Dictionary<string, object> data, List<string> errors)
            : base(data, errors)
        { }
    }
    public class ServiceResponse : Response
    {
        public int StatusCode { get; }

        public ServiceResponse(int statusCode, Dictionary<string, object> data, List<string> errors)
            : base(data, errors)
        { StatusCode = statusCode; }

        public ApiResponse Build()
        {
            return new ApiResponse( Data, Errors);
        }
    }

    abstract public class Response
    {
        public Dictionary<string, object> Data { get; }
        public List<string> Errors { get; }

        protected Response(Dictionary<string, object> data, List<string> errors)
        {
            Data = data ?? [];
            Errors = errors ?? [];
        }

    }



}
