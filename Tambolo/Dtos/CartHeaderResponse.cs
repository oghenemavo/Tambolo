using System.ComponentModel.DataAnnotations.Schema;
using Tambolo.Models;

namespace Tambolo.Dtos
{
    public class CartHeaderResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Coupon { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public ICollection<Cart> Carts { get; set; }
    }
}
