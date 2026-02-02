namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class GameSummaryDTO( // Data Transfer Object For Game Summary
        int ID,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate);
}
