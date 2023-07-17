using System.ComponentModel.DataAnnotations;
using static Tambolo.Models.Product;
using Tambolo.Models;

namespace Tambolo.Dtos
{
    public class ProductRequest
    {
        [Required] public string UserId { get; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Slug { get; set; }
        public string Description { get; set; }
        [Required] public double Amount { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
