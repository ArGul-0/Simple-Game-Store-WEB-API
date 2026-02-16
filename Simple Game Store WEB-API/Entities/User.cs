namespace Simple_Game_Store_WEB_API.Entities
{
    public class User // User Entity Class
    {
        public int ID { get; set; }
        public required string UserName { get; set; }
        public required string UserEmail { get; set; }
        public required string HashedUserPassword { get; set; }
    }
}
