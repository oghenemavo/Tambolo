using Microsoft.EntityFrameworkCore;
using Tambolo.Data;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
            await SaveAsync();
        }

        public async Task<IEnumerable<Order>> FetchAllAsync()
        {
            IEnumerable<Order> orders = await _db.Orders.ToListAsync();
            if (orders == null)
            {
                return Enumerable.Empty<Order>();
            }
            return orders;
        }

        public async Task<Order> FetchAsync(int orderId)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> RemoveAsync(int orderId)
        {
            var order = await FetchAsync(orderId);
            if (order == null)
            {
                return false;
            }
            _db.Orders.Remove(order);
            await SaveAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            await SaveAsync();
        }
    }
}
