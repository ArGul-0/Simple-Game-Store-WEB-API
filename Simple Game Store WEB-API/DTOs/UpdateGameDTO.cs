using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class UpdateGameDTO(
        [Required][StringLength(50)] string Name,
        [Required][StringLength(25)] string Genre,
        [Required][Range(0, 100)] decimal? Price,
        [Required] DateOnly? ReleaseDate);
}
