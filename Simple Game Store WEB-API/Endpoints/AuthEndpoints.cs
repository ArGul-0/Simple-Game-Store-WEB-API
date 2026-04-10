using Simple_Game_Store_WEB_API.Common.Results;
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
                    Result<string> result = await authService.Login(loginUserDTO.Email, loginUserDTO.Password);

                    return Results.Ok("Login Successful.");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }).WithName(LoginEndpointName);

            // Register Endpoint
            app.MapPost("/Register", async (RegisterUserDTO registerUserDTO, IAuthService authService, HttpContext httpContext, IConfiguration configuration) =>
            {
                try
                {
                    Result<string> loginResult = await authService.Login(registerUserDTO.Username, registerUserDTO.Password); // Optional: Check If User Already Exists Before Registering
                    if(loginResult.IsSuccess)
                    {
                        httpContext.Response.Cookies.Append(configuration["JwtOptions:NameInCookies"]!, loginResult.value); // Set The JWT Token In Cookies If User Already Exists
                        return Results.Ok("User Already Exists. Logged In Successfully.");
                    }

                    Result<string> result = await authService.Register(registerUserDTO.Username, registerUserDTO.Email, registerUserDTO.Password);
                    if(result.IsFailure)
                    {
                        return result.Error.Code switch
                        {
                            "InvalidCredentials" => Results.BadRequest(result.Error.Description), // Invalid Credentials Error
                            _ => Results.BadRequest(result.Error.Description)
                        };
                    }

                    httpContext.Response.Cookies.Append(configuration["JwtOptions:NameInCookies"]!, result.value); // Set The JWT Token In Cookies

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
