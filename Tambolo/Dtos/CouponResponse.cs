using System.ComponentModel.DataAnnotations;
using static Tambolo.Models.Coupon;

namespace Tambolo.Dtos
{
    public class CouponResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? Limit { get; set; }
        public CouponType Type { get; set; }
        public double CoupleValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
    }
}
