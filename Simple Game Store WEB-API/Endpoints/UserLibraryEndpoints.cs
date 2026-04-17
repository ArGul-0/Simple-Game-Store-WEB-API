namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class UserLibraryEndpoints // Static Class For UserLibrary Endpoints
    {
        public static RouteGroupBuilder MapUserLibraryEndpoints(this WebApplication app)
        {
            var userLibraryGroup = app.MapGroup("/UserLibrary"); // Create A Group For /UserLibrary Endpoints



            

            return userLibraryGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
