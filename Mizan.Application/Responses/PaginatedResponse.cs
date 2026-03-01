namespace Mizan.Application.Responses
{
    public class PaginatedResponse<T> : BaseResponse<T>
    {
        public int Count { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public static PaginatedResponse<T> Success(T data, int count, int take, int skip, string? message = null)
        {
            return new PaginatedResponse<T>
            {
                Succeeded = true,
                Message = message,
                Data = data,
                Count = count,
                Take = take,
                Skip = skip
            };
        }

        public new static PaginatedResponse<T> Failure(IEnumerable<string> errors, string? message = null)
        {
            return new PaginatedResponse<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors.ToList()
            };
        }

        public new static PaginatedResponse<T> Failure(string error, string? message = null)
        {
            return new PaginatedResponse<T>
            {
                Succeeded = false,
                Message = message,
                Errors = new List<string> { error }
            };
        }
    }
}
