using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tambolo.Data;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Product product)
        {
            product.Status = Product.ProductStatus.Pending;
            await _db.Products.AddAsync(product);
            await SaveAsync();
        }

        public async Task<IEnumerable<Product>> FetchAllAsync(Expression<Func<Product, bool>> filter = null)
        {
            IQueryable<Product> query = _db.Products;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            var products = await query.ToListAsync();
            if (products == null)
            {
                return Enumerable.Empty<Product>();
            }
            return products;
        }

        public async Task<Product> FetchAsync(Expression<Func<Product, bool>> filter)
        {
            return await _db.Products.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveAsync(int productId)
        {
            var product = await FetchAsync(p => p.Id == productId);
            if (product == null)
            {
                return false;
            }
            _db.Products.Remove(product);
            await SaveAsync();
            return true;
        }

        public async Task UpdateAsync(Product product)
        {
            product.UpdateDate = DateTime.UtcNow;
            _db.Products.Update(product);
            await SaveAsync();
        }

        public async Task<Dictionary<int, string>> FetchProductStatuses()
        {
            return await Task.Run(() => Enum.GetValues(typeof(Product.ProductStatus)).Cast<Product.ProductStatus>().ToDictionary(v => (int)v, k => k.ToString()));
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
