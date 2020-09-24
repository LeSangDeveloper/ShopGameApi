using System;
using System.Collections;
using System.Collections.Generic;

namespace ShopGameApi.Objects
{
    public class GameObjectJson
    {
        public int GameId;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleasedTime { get; set; }
        public double Score { get; set; }
        public string Company { get; set; }
        public List<String> Categories { get; set; }
    }
}