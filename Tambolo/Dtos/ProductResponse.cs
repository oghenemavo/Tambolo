using static Tambolo.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace Tambolo.Dtos
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProductStatus Status { get; set; }
        public bool IsActive { get; set; }
    }
}
