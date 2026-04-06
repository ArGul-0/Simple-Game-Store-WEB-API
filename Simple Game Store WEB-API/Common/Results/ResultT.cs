namespace Simple_Game_Store_WEB_API.Common.Results
{
    public class Result<T> : Result
    {
        private readonly T? Value;

        public T value => IsSuccess ? Value! : throw new InvalidOperationException("Cannot access the value of a failed result.");

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
    }
}
