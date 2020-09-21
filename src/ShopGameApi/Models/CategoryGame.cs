namespace ShopGameApi.Models
{
    public class CategoryGame
    {
        public int GameId { get; set; }
        public Game game { get; set; }

        public int CategoryId { get; set; }
        public Category category { get; set; }
    }
}