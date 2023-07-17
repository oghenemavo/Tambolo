using System.Linq.Expressions;
using Tambolo.Dtos;
using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> FetchAllAsync(Expression<Func<Cart, bool>> filter = null);
        Task<CartHeader> FetchAsync(Expression<Func<CartHeader, bool>> filter);
        //Task<Cart> FetchAsync(Expression<Func<Cart, bool>> filter);
        Task CreateAsync(CartRequest cartRequest);
        Task AddToCartAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task<bool> RemoveCartItemAsync(string userId, int cartItemId);
        Task<bool> RemoveAsync(int cartId);
        Task<bool> EmptyCartAsync(string userId);
        Task SaveAsync();
    }
}
