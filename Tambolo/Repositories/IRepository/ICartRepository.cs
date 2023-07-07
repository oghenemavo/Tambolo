using System.Linq.Expressions;
using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> FetchAllAsync(Expression<Func<Cart, bool>> filter = null);
        Task<Cart> FetchAsync(Expression<Func<Cart, bool>> filter);
        Task CreateAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task<bool> RemoveAsync(int cartId);
        Task SaveAsync();
    }
}
