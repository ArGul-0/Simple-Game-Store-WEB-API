namespace Simple_Game_Store_WEB_API.Common.Results
{
    public class Result<T> : Result
    {
        private readonly T? Value;

        public T value => IsSuccess 
            ? Value! 
            : throw new InvalidOperationException("Cannot access the value of a failed result.");

        /// <summary>
        /// Creates A Successful Result With The Provided Value.
        /// </summary>
        /// <param name="value">Value To Be Encapsulated In The Result.</param>
        private Result(T value) : base(true, Error.None)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Creates A Failed Result With The Provided Error.
        /// </summary>
        /// <param name="error">The Error Associated With The Failure.</param>
        private Result(Error error) : base(false, error)
        {
            this.Value = default;
        }

        /// <summary>
        /// Creates A Successful Result Containing The Specified Value.
        /// </summary>
        /// <param name="value">The Value To Be Encapsulated In The Successful Result.</param>
        /// <returns>A New Instance Of <see cref="Result{T}"/> Representing A Successful Operation With The Provided Value.</returns>
        public static Result<T> Success(T value) => new Result<T>(value);

        /// <summary>
        /// Creates A Failed Result With The Specified Error.
        /// </summary>
        /// <param name="error">The Error That Describes The Reason For The Failure. Cannot Be <see cref="Error.None"/>.</param>
        /// <returns>A <see cref="Result{T}"/> Representing A Failed Operation With The Provided Error.</returns>
        /// <exception cref="ArgumentException">Thrown If <paramref name="error"/> Is <see cref="Error.None"/>.</exception>
        public static Result<T> Failure(Error error)
        {
            if (error.IsNone)
                throw new ArgumentException("Error cannot be None for a failed result.", nameof(error));

            return new Result<T>(error);
        }
    }
}
