namespace Simple_Game_Store_WEB_API.Entities
{
    public class User // User Entity Class
    {
        public int ID { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public required UserLibrary Library { get; set; }
    }
}
