namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class GameDTO( // Record type for Game Data Transfer Object
        int ID,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate);
}
