namespace Simple_Game_Store_WEB_API.Common.Results
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty); // Represents No Error, Can Be Used As A Default Value
        public bool IsNone => Code == string.Empty && Description == string.Empty; // Check If This Error Represents No Error
    }
}
