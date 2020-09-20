namespace ShopGameApi.Models
{
    public class UserGame
    {
        public int UserId { get; set; }
        public User user { get; set; }

        public int GameId { get; set; }
        public Game game { get; set; }
    }
}