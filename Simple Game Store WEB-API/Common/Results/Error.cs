namespace Simple_Game_Store_WEB_API.Common.Results
{
    public sealed record Error(string code, string description)
    {
        public static readonly Error None = new Error(string.Empty, string.Empty); // Represents No Error, Can Be Used As A Default Value
        public bool IsNone => code == string.Empty; // Check If This Error Represents No Error
    }
}
