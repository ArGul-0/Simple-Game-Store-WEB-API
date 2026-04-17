namespace Simple_Game_Store_WEB_API.Entities
{
    public class UserLibrary // UserLibrary Entity Class - Represents A User's Game Library
    {
        public required int ID { get; set; }
        public required User Owner { get; set; }
        public required List<Game> Games { get; set; }
    }
}
