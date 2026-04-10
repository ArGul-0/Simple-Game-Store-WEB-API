using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs.Auth
{
    public record class LoginUserDTO(
        [Required][EmailAddress] string Email,
        [Required][StringLength(100, MinimumLength = 6)] string Password
        );
}
