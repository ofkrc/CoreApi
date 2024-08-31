namespace CoreApi.Core.Base.Model
{
    public class BaseResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;

        public BaseResponse() { }

        public BaseResponse(T data, bool success = true, string message = "")
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
