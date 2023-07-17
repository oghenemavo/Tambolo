using System.ComponentModel.DataAnnotations.Schema;

namespace Tambolo.Models
{
    public class CartHeader
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [NotMapped]
        public double Discount { get; set;}
        [NotMapped]
        public double Total { get; set;}

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public ICollection<Cart> Carts { get; } = new List<Cart>();
    }
}
