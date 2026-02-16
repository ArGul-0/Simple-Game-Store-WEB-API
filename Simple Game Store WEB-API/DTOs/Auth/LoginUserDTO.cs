using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs.Auth
{
    public record class LoginUserDTO(
        [Required][StringLength(20, MinimumLength = 3)] string Username,
        [Required][StringLength(100, MinimumLength = 6)] string Password
        );
}
