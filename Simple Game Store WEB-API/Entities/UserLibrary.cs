namespace Simple_Game_Store_WEB_API.Entities
{
    public class UserLibrary // UserLibrary Entity Class - Represents A User's Game Library
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User OwnerUser { get; set; } = null!;
        public List<UserGame> Games { get; set; } = null!;
        public UserLibrary() 
        {
            Games = new List<UserGame>();
        }
    }
}
