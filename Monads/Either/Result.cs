using System;

namespace Monads
{
    class Result<T> : Either<String, T>
    {
        Result() { }

        public Result(T t)
        {
            this.WithRight(t);
        }

        public Result<T> WithError(String error)
        {
            this.WithLeft(error);
            return this;
        }

        public static Result<T> FromError(String error)
        {
            return new Result<T>().WithError(error);
        }
    }
}
