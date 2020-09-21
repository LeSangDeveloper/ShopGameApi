using Newtonsoft.Json;
namespace ShopGameApi.Models
{
    public class CategoryGame
    {
        [JsonIgnore]
        public int GameId { get; set; }
        public Game Game { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}