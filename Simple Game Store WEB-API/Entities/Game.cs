namespace Simple_Game_Store_WEB_API.Entities
{
    public class Game // Game entity class
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public Genre? Genre { get; set; }
        public int GenreID { get; set; }
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
