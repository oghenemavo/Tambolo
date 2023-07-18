using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Tambolo.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Product> Products { get; } = new List<Product>();
        public ICollection<Cart> Carts { get; } = new List<Cart>();
    }
}
