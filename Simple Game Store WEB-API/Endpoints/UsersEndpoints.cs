using Microsoft.EntityFrameworkCore;
using Simple_Game_Store_WEB_API.Common.Results;
using Simple_Game_Store_WEB_API.Data;
using Simple_Game_Store_WEB_API.DTOs.Users;
using Simple_Game_Store_WEB_API.Entities;
using Simple_Game_Store_WEB_API.Mappers;
using Simple_Game_Store_WEB_API.Services.UserLibrary;
using Simple_Game_Store_WEB_API.Services.Users;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class UsersEndpoints // Static Class For User Endpoints
    {
        const string GetMyIDEndpointName = "GetMyID"; // Constant For The Get My ID Endpoint Name
        const string GetGamesFromLibraryEndpointName = "GetGamesFromLibrary"; // Constant For The Get Games From Library Endpoint Name
        const string AddGameToUserLibraryEndpointName = "AddGameToUserLibrary"; // Constant For The Add Game To User Library Endpoint Name
        const string RemoveGameFromUserLibraryEndpointName = "RemoveGameFromUserLibrary"; // Constant For The Remove Game From User Library Endpoint Name

        /// <summary>
        /// Maps The Users Endpoints To The Web Application
        /// </summary>
        /// <remarks>
        /// This Method Sets Up The Following Endpoints Under The /Users Route:
        /// - GET /Users/GetMyID: Retrieve The Current User's ID
        /// - GET /Users/{userID}/GetGamesFromLibrary: Retrieve All Games From A User's Library
        /// - POST /Users/{userID}/AddGameToUserLibrary: Add A Game To A User's Library
        /// - DELETE /Users/{userID}/RemoveGameFromUserLibrary: Remove A Game From A User's Library
        /// </remarks>
        public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
        {
            var usersGroup = app.MapGroup("/Users/"); // Create A Group For /Users Endpoints



            // Endpoint To Get The Current User's ID From The JWT Token
            usersGroup.MapGet("/GetMyID", async (HttpContext httpContext) =>
            {
                return Results.Ok(int.Parse(httpContext.User.FindFirst("userID")?.Value ?? "0"));
            }).WithName(GetMyIDEndpointName).RequireAuthorization();

            // Get All User's Games From Library
            usersGroup.MapGet("/{userID}/GetGamesFromLibrary", async (int userID, GameStoreContext dbContext, IUsersMapper usersMapper, HttpContext httpContext) =>
            {
                User? user = await dbContext.Users.FindAsync(userID);

                if (user is null)
                    return Results.NotFound(UsersErrors.UserNotFound.Description);

                var userLibrary = await dbContext.UserLibraries
                    .AsNoTracking()
                    .Include(ul => ul.Games)
                        .ThenInclude(g => g.Game)
                    .FirstOrDefaultAsync(ul => ul.UserID == userID);

                if (userLibrary is null)
                    return Results.Ok(new List<UserGameDTO>());

                List<UserGameDTO> games = userLibrary.Games.Select(ug => usersMapper.ToDTO(ug)).ToList();

                return Results.Ok(games);
            }).WithName(GetGamesFromLibraryEndpointName).RequireAuthorization();

            // Add A Game To The Current User's Library
            usersGroup.MapPost("/{userID}/AddGameToUserLibrary", async (int userID, int gameID, IUserService userService, HttpContext httpContext) =>
            {
                if(userID != int.Parse(httpContext.User.FindFirst("userID")?.Value ?? "0"))
                    return Results.Forbid();

                Result result = await userService.AddGameToUserLibraryAsync(userID, gameID);

                if(result.IsFailure)
                {
                    return result.Error.Code switch
                    {
                        "UserNotFound" => Results.NotFound(result.Error.Description),
                        "GameNotFound" => Results.NotFound(result.Error.Description),
                        "GameAlreadyInLibrary" => Results.BadRequest(result.Error.Description),

                        _ => Results.BadRequest("An Unknown Error Occurred While Adding The Game To The User's Library")
                    };
                }

                return Results.Ok();
                    
            }).WithName(AddGameToUserLibraryEndpointName).RequireAuthorization();

            // Remove A Game From The Current User's Library
            usersGroup.MapDelete("/{userID}/RemoveGameFromLibrary", async (int userID, int gameID, IUserService userService, HttpContext httpContext) =>
            {
                if (userID != int.Parse(httpContext.User.FindFirst("userID")?.Value ?? "0"))
                    return Results.Forbid();

                Result result = await userService.RemoveGameFromUserLibraryAsync(userID, gameID); 
            
            if(result.IsFailure)
            {
                return result.Error.Code switch
                {
                    "UserNotFound" => Results.NotFound(result.Error.Description),
                    "GameNotFound" => Results.NotFound(result.Error.Description),

                    _ => Results.BadRequest("An Unknown Error Occurred While Removing The Game From The User's Library")
                };
            }

            return Results.Ok();
            }).WithName(RemoveGameFromUserLibraryEndpointName).RequireAuthorization();

            return usersGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
