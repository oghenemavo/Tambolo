using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tambolo.Data;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;
        public CartRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Cart cart)
        {
            await _db.Carts.AddAsync(cart);
            await SaveAsync();
        }

        public async Task<IEnumerable<Cart>> FetchAllAsync(Expression<Func<Cart, bool>> filter = null)
        {
            IQueryable<Cart> query = _db.Carts;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Cart> FetchAsync(Expression<Func<Cart, bool>> filter)
        {
            return await _db.Carts.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> RemoveAsync(int cartId)
        {
            var cart = await FetchAsync(c => c.Id == cartId);
            if (cart == null)
            {
                return false;
            }
            _db.Carts.Remove(cart);
            await SaveAsync();
            return true;
        }

        public async Task UpdateAsync(Cart cart)
        {
            cart.UpdatedDate = DateTime.UtcNow;
            _db.Carts.Update(cart);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}