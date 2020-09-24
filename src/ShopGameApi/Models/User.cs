using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ShopGameApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        [StringLength(255, MinimumLength = 4)]
        public string Name { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(255, MinimumLength = 5)]
        public string Password { get; set; }

        [XmlIgnore]
        public IList<UserGame> UserGame { get; set; }
    }
}