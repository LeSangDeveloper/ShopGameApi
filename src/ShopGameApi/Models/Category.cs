using System.Collections.Generic;

namespace ShopGameApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public IList<CategoryGame> CategoryGame { get; set; }
    }
}