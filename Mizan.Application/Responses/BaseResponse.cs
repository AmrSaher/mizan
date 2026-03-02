namespace Mizan.Application.Responses
{
    public class BaseResponse
    {
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }

        public static BaseResponse Success(string? message = null)
        {
            return new BaseResponse
            {
                Succeeded = true,
                Message = message
            };
        }

        public static BaseResponse Failure(IEnumerable<string> errors, string? message = null)
        {
            return new BaseResponse
            {
                Succeeded = false,
                Message = message,
                Errors = errors.ToList()
            };
        }

        public static BaseResponse Failure(string error, string? message = null)
        {
            return new BaseResponse
            {
                Succeeded = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }

        public static BaseResponse<T> Success(T data, string? message = null)
        {
            return new BaseResponse<T>
            {
                Succeeded = true,
                Message = message,
                Data = data
            };
        }

        public new static BaseResponse<T> Failure(IEnumerable<string> errors, string? message = null)
        {
            return new BaseResponse<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors.ToList()
            };
        }

        public new static BaseResponse<T> Failure(string error, string? message = null)
        {
            return new BaseResponse<T>
            {
                Succeeded = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }
}
