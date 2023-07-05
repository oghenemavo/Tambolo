using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> FetchAllAsync();
        Task<Category> FetchAsync(int categoryId);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task<bool> RemoveAsync(int categoryId);
        Task SaveAsync();

    }
}
