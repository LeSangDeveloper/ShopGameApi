using System.ComponentModel.DataAnnotations;

namespace ShopGameApi.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(255, MinimumLength = 3)]
        public string Country { get; set; }
    }
}