namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class GameDetailsDTO( // Data Transfer Object For Game Details
        int ID,
        string Name,
        int GenreID,
        decimal Price,
        DateOnly ReleaseDate);
}
