namespace Back_End.Models.ResponseModels
{
    public class ApiResponseModel
    {
        public Dictionary<string, object> Data { get; }
        public List<string> Errors { get; }

        public ApiResponseModel(Dictionary<string, object> data, List<string> errors)
        {
            Data = data ?? [];
            Errors = errors ?? [];
        }
    }
}
