using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Tambolo.Dtos;
using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FetchAllAsync(Expression<Func<Product, bool>> filter = null);
        Task<Product> FetchAsync(Expression<Func<Product, bool>> filter);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> RemoveAsync(int productId);
        Task<Dictionary<int, string>> FetchProductStatuses();
        Task SaveAsync();
    }
}
