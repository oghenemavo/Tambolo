using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using Tambolo.Data;
using Tambolo.Dtos;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CartRepository(IMapper mapper, AppDbContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddToCartAsync(Cart cartEntity)
        {
            var cartItem = await FetchAsync(c => c.UserId == cartEntity.UserId && c.ProductId == cartEntity.ProductId);

            if (cartEntity.Quantity > 0 && cartItem == null) 
            {
                await _db.Carts.AddAsync(cartEntity);
            } 
            else if (cartEntity.Quantity > 0 && cartItem != null)
            {
                cartItem.Quantity = cartEntity.Quantity;
                cartItem.UpdatedDate = DateTime.Now;
                _db.Carts.Update(cartItem);
            }
            else if (cartEntity.Quantity < 1 && cartItem != null)
            {
                _db.Carts.Remove(cartItem);
            }
            await SaveAsync();
        }

        public async Task<IEnumerable<Cart>> FetchAllAsync(Expression<Func<Cart, bool>> filter = null)
        {
            IQueryable<Cart> query = _db.Carts;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.Include(c => c.Product).ToListAsync();
        }

        public async Task<Cart?> FetchAsync(Expression<Func<Cart, bool>> filter)
        {
            return await _db.Carts.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> EmptyCartAsync(string userId)
        {
            var cartItems = await FetchAllAsync(c => c.UserId == userId);
            if (cartItems != null)
            {
                foreach (var cartItem in cartItems)
                {
                    // remove cart items
                    _db.Carts.Remove(cartItem);
                    await SaveAsync();
                }
                await SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveAsync(string userId, int cartItemId)
        {
            var cartItem = await _db.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == cartItemId);
            if (cartItem == null)
            {
                return false;
            }
            _db.Carts.Remove(cartItem);
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

        public async Task<object> ApplyCouponAsync(string userId, string couponCode)
        {
            var cartItems = await FetchAllAsync(ch => ch.UserId == userId);
            double total = 0;
            double discount = 0;
            var couponResponse = new { Total = total, Discount = discount, Status = false };

            if (cartItems != null)
            {
                var coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.Code == couponCode);
                if (coupon != null)
                {
                    // TODO: check coupon date is valid & coupon used time is valid
                    foreach (var item in cartItems)
                    {
                        total += item.Product.Amount * item.Quantity;
                    }

                    if (coupon.Type == Coupon.CouponType.Percentage)
                    {
                        discount = (coupon.CoupleValue / 100) * total;
                    }
                    else
                    {
                        if (coupon.CoupleValue <= total)
                        {
                            discount = total - coupon.CoupleValue;
                        }
                    }
                    couponResponse = new { Total = total, Discount = discount, Status = true };
                }
            }
            return couponResponse;
        }
    }
}