using System.Linq.Expressions;
using Tambolo.Dtos;
using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> FetchAllAsync(Expression<Func<Cart, bool>> filter = null);
        Task<Cart> FetchAsync(Expression<Func<Cart, bool>> filter);
        Task AddToCartAsync(Cart cartEntity);
        Task UpdateAsync(Cart cart);
        Task<bool> RemoveAsync(string userId, int cartItemId);
        Task<bool> EmptyCartAsync(string userId);
        Task<bool> ApplyCouponAsync(string userId, string couponCode);
        Task SaveAsync();
    }
}
