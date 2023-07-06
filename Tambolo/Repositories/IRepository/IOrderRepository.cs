using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> FetchAllAsync();
        Task<Order> FetchAsync(int orderId);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task<bool> RemoveAsync(int orderId);
        Task SaveAsync();
    }
}
