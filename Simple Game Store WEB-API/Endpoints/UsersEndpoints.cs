using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.Entities;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class UsersEndpoints // Static Class For User Endpoints
    {
        public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
        {
            var usersGroup = app.MapGroup("/Users/"); // Create A Group For /Users Endpoints

            usersGroup.MapGet("/GetMyID", async (HttpContext httpContext) =>
            {
                return Results.Ok(int.Parse(httpContext.User.FindFirst("userID")?.Value ?? "0"));
            }).RequireAuthorization();


            return usersGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
