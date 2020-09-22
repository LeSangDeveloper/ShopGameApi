using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;
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