using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        public Company company { get; set; }

        public IList<UserGame> UserGame { get; set; }

    }
}