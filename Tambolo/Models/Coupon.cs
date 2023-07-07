using System.ComponentModel.DataAnnotations;

namespace Tambolo.Models
{
    public class Coupon
    {
        public enum CouponType
        {
            Fixed = 1,
            Percentage = 2
        }

        [Key] public int Id { get; set; }

        public int? ProductId { get; set; } = null;
        public Product Product { get; set; } = null;

        [Required] public string Title { get; set; }
        [Required] public string Code { get; set; }
        public int? Limit { get; set; } = null;
        public CouponType Type { get; set; } = CouponType.Fixed;
        [Required] public double CoupleValue { get; set; }
        [Required] public DateTime StartDate { get; set; }
        [Required] public DateTime EndDate { get; set; }
        [Required] public int Status { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
