using System;

namespace ShopManager.Controller.ResultHandler
{
    public class ValueResult<TValue> : Result
    {
        public TValue Value
        {
            get => IsSuccess ? _value : throw new InvalidOperationException("Can't get value when the result failed!");
        }
        private readonly TValue _value;

        private ValueResult(TValue value) : base()
        {
            _value = value;
        }

        private ValueResult(Error error) : base(error)
        { }

        public static ValueResult<TValue> Successful(TValue value)
        {
            return new ValueResult<TValue>(value);
        }

        public static new ValueResult<TValue> Failed(Error error)
        {
            return new ValueResult<TValue>(error);
        }

        public T Match<T>(Func<TValue, T> onSuccess, Func<Error, T> onFailure)
        {
            return IsSuccess ? onSuccess(Value) : onFailure(ResultingError);
        }
    }
}
