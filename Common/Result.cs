


namespace Common.DTOs
{

    public class Result
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
      

        public Result(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public static Result Success()
        {
            return new Result(true, string.Empty);
        }
        
        public static Result Success(string message)
        {
            return new Result(true, message);
        }

        public static ResultError Fail()
        {
            return new ResultError(string.Empty);
        }

        public static ResultError Fail(string message)
        {
            return new ResultError(message);
        }

        public static ResultError Fail(string message, List<string> errors)
        {
            return new ResultError(message, errors);
        }
    }

    public class ResultError : Result
    {
        public List<string> Errors { get; set; } = new();

        public ResultError(string message) : base(false, message)
        {
        }

        public ResultError(string message, List<string> errors) : base(false, message)
        {
            Errors = errors;
        }
    }


    public class Result<T> : Result
    {
        public T Data { get; set; }
        
        public Result(T data, bool succeeded, string message) : base(succeeded, message)
        {
            Data = data;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data, true, string.Empty);
        }

        public static Result<T> Success(T data, string message)
        {
            return new Result<T>(data, true, message);
        }

        public static ResultError<T> Fail(T data)
        {
            return new ResultError<T>(data, string.Empty);
        }

        public static ResultError<T> Fail(T data, string message)
        {
            return new ResultError<T>(data, message);
        }

        public static ResultError<T> Fail(T data, string message, List<string> errors)
        {
            return new ResultError<T>(data, message, errors);
        }
    }

    public class ResultError<T> : Result<T>
    {
        public List<string> Errors { get; set; } = new();

        public ResultError(T data, string message) : base(data, false, message)
        {
        }

        public ResultError(T data, string message, List<string> errors) : base(data, false, message)
        {
            Errors = errors;
        }
    }

}
