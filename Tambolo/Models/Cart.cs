using System.ComponentModel.DataAnnotations.Schema;
using Tambolo.Dtos;

namespace Tambolo.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int CartHeaderId { get; set; }
        public CartHeader CartHeader { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public int Quantity { get; set; }

        [NotMapped]
        public ProductResponse ProductInfo { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
