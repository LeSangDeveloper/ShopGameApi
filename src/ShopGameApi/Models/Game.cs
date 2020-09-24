using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ShopGameApi.Objects;

namespace ShopGameApi.Models
{
    public class Game
    {
        public int GameId { get; set; }
        
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; } 
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.DateTime)]
        public DateTime ReleasedTime { get; set; }
        
        public Company Company { get; set; }

        public Rating Rating { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public IList<UserGame> UserGame { get; set; }

        [XmlIgnore]
        public IList<CategoryGame> CategoryGame { get; set; }

        public GameObjectJson Covert()
        {
            GameObjectJson gameObjectJson = new GameObjectJson();
            gameObjectJson.Name = this.Name;
            gameObjectJson.Price = this.Price;
            gameObjectJson.Company = this.Company.Name;
            gameObjectJson.ReleasedTime = this.ReleasedTime;
            gameObjectJson.Score = this.Rating.Score;
            gameObjectJson.Categories = new List<string>();

            foreach (CategoryGame categoryGame in CategoryGame)
            {
                gameObjectJson.Categories.Add(categoryGame.Category.Name);
            }

            return gameObjectJson;
        }

    }
}