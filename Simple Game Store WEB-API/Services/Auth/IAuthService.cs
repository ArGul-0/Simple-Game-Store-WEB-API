namespace Simple_Game_Store_WEB_API.Services.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Login Is Used To Authenticate A User With The Provided Username And Password. It Validates The Credentials Against The User Store And Establishes An Authenticated Session If The Credentials Are Valid. If The Authentication Is Successful, It May Generate An Authentication Token Or Set A Cookie To Maintain The User's Authenticated State For Subsequent Requests.
        /// </summary>
        public Task Login(string username, string password);
        /// <summary>
        /// Register Is Used To Create A New User Account With The Provided Username And Password. It Validates The Input Data, Ensures That The Username Is Unique, And Stores The User's Credentials In The User Store. After Successful Registration, The User Can Log In Using The Registered Credentials To Access Protected Resources Or Perform Authenticated Actions Within The Application.
        /// </summary>
        public Task Register(string username, string password);
    }
}
