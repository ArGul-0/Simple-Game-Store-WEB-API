using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class UpdateGameDTO( // Record type for updating a game
        [Required][StringLength(50)] string Name,
        [Required] int GenreID,
        [Required][Range(0, 100)] decimal Price,
        [Required] DateOnly ReleaseDate);
}
