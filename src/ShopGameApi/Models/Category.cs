using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ShopGameApi.Objects;

namespace ShopGameApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        [XmlIgnore]
        public IList<CategoryGame> CategoryGame { get; set; }
        
        public CategoryObjectJson Convert()
        {
            CategoryObjectJson categoryObject = new CategoryObjectJson();
            categoryObject.CategoryId = this.CategoryId;
            categoryObject.Name = this.Name;
            categoryObject.Games = new List<string>();
            
            foreach (CategoryGame categoryGame in CategoryGame)
            {
                categoryObject.Games.Add(categoryGame.Game.Name);
            }

            return categoryObject;
        }
    }
}