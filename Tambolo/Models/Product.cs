using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tambolo.Models
{
    public class Product
    {
        public enum ProductStatus
        {
            Pending,
            Approved,
            Disapproved,
            Suspended
        }


        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; } = 0.00;
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Pending;
        public bool IsActive { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public ICollection<Order> Orders { get; } = new List<Order>();
        public ICollection<Cart> Carts { get; } = new List<Cart>();
        public ICollection<Coupon> Coupons { get; } = new List<Coupon>();
    }
}
