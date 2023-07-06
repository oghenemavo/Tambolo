using Microsoft.EntityFrameworkCore;
using Tambolo.Data;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Category?> FetchAsync(int categoryId)
        {
            return await _db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<IEnumerable<Category>> FetchAllAsync()
        {
            var category = await _db.Categories.ToListAsync();
            if (category == null)
            {
                return Enumerable.Empty<Category>();
            }
            return category;
        }

        public async Task CreateAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await SaveAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            category.UpdateDate = DateTime.UtcNow;
            _db.Categories.Update(category);
            await SaveAsync();
        }

        public async Task<bool> RemoveAsync(int categoryId)
        {
            var category = await FetchAsync(categoryId);
            if (category == null)
            {
                return false;
            }
            _db.Categories.Remove(category);
            await SaveAsync();
            return true;
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
