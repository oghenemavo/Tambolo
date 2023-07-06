using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Tambolo.Models
{
    [Index(nameof(Title), IsUnique = true)]
    [Index(nameof(Slug), IsUnique = true)]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;

        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
