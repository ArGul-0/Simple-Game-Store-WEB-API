using Simple_Game_Store_WEB_API.DTOs;

namespace Simple_Game_Store_WEB_API.Entities
{
    public class UserGame // UserGame Entity Class - Represents A Game Owned By A User In Their Library
    {
        public int ID { get; set; }

        // FK to UserLibrary
        public int UserLibraryID { get; set; }
        public UserLibrary UserLibrary { get; set; } = null!;

        // Reference to Game entity (store GameId and navigation)
        public int GameID { get; set; }
        public Game Game { get; set; } = null!;

        public DateTime PurchasedAt { get; set; }
    }
}
