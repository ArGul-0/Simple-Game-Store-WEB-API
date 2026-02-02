using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class CreateGameDTO( // Data Transfer Object For Creating A New Game
        [Required][StringLength(50)] string Name,
        [Required][Range(1, int.MaxValue)] int GenreID,
        [Required][Range(0, 100)] decimal Price,
        [Required] DateOnly ReleaseDate);
}
