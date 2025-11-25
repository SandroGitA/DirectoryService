//namespace Shared
//{
//    public class Result
//    {
//        public bool IsSuccess { get; }

//        public bool IsFailure => !IsSuccess;

//        public Error Error { get; }

//        protected Result()
//        {
//            IsSuccess = true;
//            Error = Error.None;
//        }

//        protected Result(Error error)
//        {
//            IsSuccess = false;
//            Error = error;
//        }

//        public static implicit operator Result(Error error) => Failure(error);

//        public static Result Failure(Error error)
//        {
//            return new Result(error);
//        }        

//        public static Result Success()
//        {
//            return new Result();
//        }
//    }

//    public sealed class Result<TValue> : Result
//    {
//        private readonly TValue value = default!;

//        private Result(TValue value)
//        {
//            this.value = value;
//        }

//        private Result(Error error) : base(error) { }

//        public new static Result<TValue> Failure(Error error)
//        {
//            return new Result<TValue>(error);
//        }

//        public static Result<TValue> Success(TValue value)
//        {
//            return new Result<TValue>(value);
//        }

//        public static implicit operator Result<TValue>(Error error) => Failure(error);

//        public static implicit operator Result<TValue>(TValue value) => Success(value);

//        public static implicit operator TValue(Result<TValue> value) => value.Value;

//        public TValue Value => IsSuccess ? value : throw new ApplicationException("Result is not success");
//    }
//}
