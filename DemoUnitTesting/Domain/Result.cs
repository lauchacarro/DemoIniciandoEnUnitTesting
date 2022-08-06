namespace DemoUnitTesting.Domain
{
    public class Result
    {

        public Result(bool succeeded, string? error)
        {
            Succeeded = succeeded;
            Error = error;
        }

        public Result()
        {
        }

        public bool Succeeded { get; set; }

        public string? Error { get; set; }

        public static Result Success
            => new Result(true, null);

        public static Result Failure(string error)
            => new Result(false, error);


        public static implicit operator Result(string error)
            => Failure(error);

        public static implicit operator Result(bool success)
            => success ? Success : Failure("ERR001");

        public static implicit operator bool(Result result)
            => result.Succeeded;
    }

    public class Result<TData> : Result
    {

        public Result()
        {
        }


        public Result(bool succeeded, TData? data, string? error)
            : base(succeeded, error)
            => this.Data = data;

        public Result(bool succeeded, string? error) : base(succeeded, error)
        {
        }

        public TData? Data { get; set; }

        public static Result<TData> SuccessWith(TData data)
            => new Result<TData>(true, data, null);

        public new static Result<TData> Failure(string error)
            => new Result<TData>(false, default, error);

        //public static implicit operator Result<TData>(string error)
        //    => Failure(new List<string> { error });

        public static implicit operator Result<TData>(string error)
            => Failure(error);

        public static implicit operator Result<TData>(TData data)
            => SuccessWith(data);

        public static implicit operator bool(Result<TData> result)
            => result.Succeeded;
    }


}
