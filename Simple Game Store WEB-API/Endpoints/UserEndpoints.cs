namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class UserEndpoints // Static Class For User Endpoints
    {
        public static RouteGroupBuilder MapUserEndpoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("/User"); // Create A Group For /User Endpoints


            

            return userGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
