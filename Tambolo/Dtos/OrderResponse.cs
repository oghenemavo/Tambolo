using static Tambolo.Models.Product;
using System.ComponentModel.DataAnnotations;
using Tambolo.Models;

namespace Tambolo.Dtos
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Email { get; set; }
        public string Meta { get; set; }
        public double Amount { get; set; } = 0.00;
        public bool Status { get; set; }
    }
}
