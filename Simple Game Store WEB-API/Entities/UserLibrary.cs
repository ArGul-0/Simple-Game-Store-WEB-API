namespace Simple_Game_Store_WEB_API.Entities
{
    public class UserLibrary // UserLibrary Entity Class - Represents A User's Game Library
    {
        public int ID { get; set; }
        public List<Game> Games = new List<Game>();
    }
}
