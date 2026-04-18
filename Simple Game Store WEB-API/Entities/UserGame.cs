namespace Simple_Game_Store_WEB_API.Entities
{
    public class UserGame // UserGame Entity Class - Represents A Game Owned By A User In Their Library
    {
        public int ID { get; set; }

        public int LibraryID { get; set; }
        public UserLibrary UserLibrary { get; set; } = null!;

        public int GameID { get; set; }
        public required Game Game { get; set; }

        public required DateTime PurchasedAt { get; set; }
    }
}
