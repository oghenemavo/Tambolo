using System.ComponentModel.DataAnnotations;
using static Tambolo.Models.Product;
using Tambolo.Models;
using static Tambolo.Models.Coupon;

namespace Tambolo.Dtos
{
    public class CouponUpdateRequest
    {
        [Required] public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Code { get; set; }
        public int? Limit { get; set; }
        public CouponType Type { get; set; }
        [Required] public double CoupleValue { get; set; }
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }
}
