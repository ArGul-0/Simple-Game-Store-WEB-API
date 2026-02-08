using System.ComponentModel.DataAnnotations;

namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class UpdateGenreDTO( // Data Transfer Object For Updating An Existing Genre
        [Required][StringLength(50)] string Name);
}
