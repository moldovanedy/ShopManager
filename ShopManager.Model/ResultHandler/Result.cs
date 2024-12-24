using System;

namespace ShopManager.Controller.ResultHandler
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error ResultingError { get; }

        protected Result()
        {
            IsSuccess = true;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            ResultingError = error;
        }

        public static Result Succesful()
        {
            return new Result();
        }

        public static Result Failed(Error error)
        {
            return new Result(error);
        }

        public T Match<T>(Func<T> onSuccess, Func<Error, T> onFailure)
        {
            return IsSuccess ? onSuccess() : onFailure(ResultingError);
        }
    }
}
