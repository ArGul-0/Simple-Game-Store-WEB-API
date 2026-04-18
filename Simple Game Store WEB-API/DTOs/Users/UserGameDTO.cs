using Simple_Game_Store_WEB_API.Entities;
using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs.Users
{
    public record class UserGameDTO(
        [Required] GameDetailsDTO Game,
        [Required] DateTime PurchasedAt
        );
}
