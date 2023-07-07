using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tambolo.Data;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _db;
        public CouponRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Coupon coupon)
        {
            _db.Coupons.Add(coupon);
            await SaveAsync();
        }

        public async Task<IEnumerable<Coupon>> FetchAllAsync(Expression<Func<Coupon, bool>> expression = null)
        {
            IQueryable<Coupon> query = _db.Coupons;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await query.ToListAsync();
        }

        public async Task<Coupon> FetchAsync(Expression<Func<Coupon, bool>> expression)
        {
            return await _db.Coupons.FirstOrDefaultAsync(expression);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Coupon coupon)
        {
            _db.Coupons.Update(coupon);
            await SaveAsync();
        }
    }
}
