using System.ComponentModel.DataAnnotations;
using static Tambolo.Models.Product;
using Tambolo.Models;

namespace Tambolo.Dtos
{
    public class OrderUpdateRequest
    {
        [Required] public int Id { get; set; }
        [Required] public int ProductId { get; set; }
        [Required] public string Email { get; set; }
        public string Meta { get; set; }
        [Required] public double Amount { get; set; }
        public bool Status { get; set; }
    }
}
