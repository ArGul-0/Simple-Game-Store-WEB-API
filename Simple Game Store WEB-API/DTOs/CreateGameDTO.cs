namespace Simple_Game_Store_WEB_API.DTOs
{
    public record class CreateGameDTO(
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate);
}
