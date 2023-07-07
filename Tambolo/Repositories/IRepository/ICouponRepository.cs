using System.Linq.Expressions;
using Tambolo.Models;

namespace Tambolo.Repositories.IRepository
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> FetchAllAsync(Expression<Func<Coupon, bool>> expression = null);
        Task<Coupon> FetchAsync(Expression<Func<Coupon, bool>> expression);
        Task CreateAsync(Coupon coupon);
        Task UpdateAsync(Coupon coupon);
        Task SaveAsync();
    }
}
