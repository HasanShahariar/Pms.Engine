namespace PMS.Application.Common.Models
{
    public class Result : IResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public string[] Errors { get; private set; }

        private Result(bool success, string message, string[] errors = null)
        {
            Success = success;
            Message = message;
            Errors = errors ?? Array.Empty<string>();
        }

        public static Result CreateSuccess(string message = "Operation completed successfully.")
        {
            return new Result(true, message);
        }

        public static Result CreateFailure(string message, params string[] errors)
        {
            return new Result(false, message, errors);
        }
    }

    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
        string[] Errors { get; }
    }
}
