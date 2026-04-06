namespace Simple_Game_Store_WEB_API.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if(isSuccess && !error.IsNone)
                throw new InvalidOperationException("A successful result cannot have an error.");
            if(!isSuccess && error.IsNone)
                throw new InvalidOperationException("A failed result must have an error.");

            this.IsSuccess = isSuccess;
            this.Error = error;
        }

        /// <summary>
        /// Creates A Successful Result.
        /// </summary>
        /// <returns>A Successful Result.</returns>
        public static Result Success() => new Result(true, Error.None);

        /// <summary>
        /// Creates A Failed Result With The Provided Error.
        /// </summary>
        /// <returns>A Failed Result With The Provided Error.</returns>
        public static Result Failure(Error error)
        {
            if(error.IsNone)
                throw new InvalidOperationException("Error cannot be None for a failure result.");

            return new Result(false, error);
        }
    }
}
