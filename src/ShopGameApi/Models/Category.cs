using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
namespace ShopGameApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        [XmlIgnore]
        public IList<CategoryGame> CategoryGame { get; set; }
    }
}