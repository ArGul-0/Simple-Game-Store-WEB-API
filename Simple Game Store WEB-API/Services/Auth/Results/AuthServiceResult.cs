namespace Simple_Game_Store_WEB_API.Services.Auth.Results
{
    public enum AuthServiceResult
    {
        Success, // Indicates That The Authentication Operation Was Successful
        InvalidCredentials, // Indicates That The Provided Credentials Are Invalid (e.g., Incorrect Email Or Password)
        UserNotFound, // Indicates That No User Was Found With The Provided Email During Login
        Error // Indicates That An Unexpected Error Occurred During The Authentication Process
    }
}
