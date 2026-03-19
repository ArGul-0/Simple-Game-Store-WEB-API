using Simple_Game_Store_WEB_API.Services.Auth;

namespace Simple_Game_Store_WEB_API.Endpoints
{
    public static class AuthEndpoints // Static Class For Auth Endpoints
    {
        const string LoginEndpointName = "Login"; // Constant For The Login Endpoint Name
        const string RegisterEndpointName = "Register"; // Constant For The Register Endpoint Name

        public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
        {
            var authGroup = app.MapGroup("/Auth"); // Create A Group For /Auth Endpoints



            // Login Endpoint
            app.MapPost("/Login", async (string username, string password, IAuthService authService) =>
            {
                try
                {
                    await authService.Login(username, password);

                    return Results.Ok("Login Successful.");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(LoginEndpointName);

            // Register Endpoint
            app.MapPost("/Register", async (string username, string email, string password, IAuthService authService) =>
            {
                try
                {
                    await authService.Login(username, password); // Optional: Check If User Already Exists Before Registering
                    await authService.Register(username, email, password);

                    return Results.Ok("Registration Successful.");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(RegisterEndpointName);

            return authGroup; // Return The Group For Further Configuration If Needed
        }
    }
}
