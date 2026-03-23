using Simple_Game_Store_WEB_API.Services.Auth;
using Simple_Game_Store_WEB_API.DTOs.Auth;

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
            app.MapPost("/Login", async (LoginUserDTO loginUserDTO, IAuthService authService) =>
            {
                try
                {
                    await authService.Login(loginUserDTO.Username, loginUserDTO.Password);

                    return Results.Ok("Login Successful.");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(LoginEndpointName);

            // Register Endpoint
            app.MapPost("/Register", async (RegisterUserDTO registerUserDTO, IAuthService authService, HttpContent httpContent) =>
            {
                try
                {
                    await authService.Login(registerUserDTO.Username, registerUserDTO.Password); // Optional: Check If User Already Exists Before Registering
                    await authService.Register(registerUserDTO.Username, registerUserDTO.Email, registerUserDTO.Password);



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
