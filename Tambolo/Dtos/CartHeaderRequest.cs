using System.ComponentModel.DataAnnotations.Schema;
using Tambolo.Models;

namespace Tambolo.Dtos
{
    public class CartHeaderRequest
    {
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
