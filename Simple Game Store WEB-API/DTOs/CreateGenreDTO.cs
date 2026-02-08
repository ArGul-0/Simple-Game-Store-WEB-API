using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class CreateGenreDTO( // Data Transfer Object For Creating A New Genre
        [Required][StringLength(50)] string Name);
}
