namespace Simple_Game_Store_WEB_API.Entities
{
    public class UserGame
    {
        public required int LibraryID { get; set; }
        public required UserLibrary UserLibrary { get; set; }

        public required int GameID { get; set; }
        public required Game Game { get; set; }

        public DateTime PurchasedAt { get; set; }
    }
}
