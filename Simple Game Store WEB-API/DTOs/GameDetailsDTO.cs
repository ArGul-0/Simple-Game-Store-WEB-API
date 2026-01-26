namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class GameDetailsDTO(
        int ID,
        string Name,
        int GenreID,
        decimal Price,
        DateOnly ReleaseDate);
}
