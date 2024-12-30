using System;

namespace ShopManager.Controller.ResultHandler
{
    public class Result
    {
        public bool IsSuccess { get; }
        /// <summary>
        /// Returns the error or null if no error occurred.
        /// </summary>
        public Error ResultingError { get; } = null;

        protected Result()
        {
            IsSuccess = true;
        }

        protected Result(Error error)
        {
            IsSuccess = false;
            ResultingError = error;
        }

        public static Result Successful()
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
