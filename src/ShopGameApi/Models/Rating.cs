using System.Collections.Generic;

namespace ShopGameApi.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public double Score { get; set; }
        public int Quantity { get; set; }
    }
}