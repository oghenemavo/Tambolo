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

        public async Task CreateAsync(CartRequest cartRequest)
        {
            var cart = await FetchAsync(cH => cH.UserId == cartRequest.UserId);
            var cartItem = _mapper.Map<Cart>(cartRequest);
            if (cart == null)
            {
                // TODO: setup db transactions

                // create cart header
                var cartHeader = _mapper.Map<CartHeader>(cartRequest);
                await _db.CartHeaders.AddAsync(cartHeader);
                await SaveAsync();

                // add item
                cartItem.CartHeaderId = cartHeader.Id;
                await AddToCartAsync(cartItem);
            }
            else
            {
                cartItem.CartHeaderId = cart.Id;
                await AddToCartAsync(cartItem);
            }
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

        public async Task<CartHeader> FetchAsync(Expression<Func<CartHeader, bool>> filter)
        {
            return await _db.CartHeaders.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> EmptyCartAsync(string userId)
        { 
            var cartHeader = await FetchAsync(ch => ch.UserId == userId);
            if (cartHeader != null)
            {
                var cartItems = await _db.Carts.Where(c => c.CartHeaderId == cartHeader.Id).ToListAsync();
                foreach (var cartItem in cartItems)
                {
                    // remove cart items
                    _db.Carts.Remove(cartItem);
                    await SaveAsync();
                }

                // remove cartheader
                _db.CartHeaders.Remove(cartHeader);
                await SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveAsync(int cartId)
        {
            var cart = await _db.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
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

        public async Task AddToCartAsync(Cart cart)
        {
            var cartItem = await _db.Carts.FirstOrDefaultAsync(c => c.CartHeaderId == cart.CartHeaderId && c.ProductId == cart.ProductId);
            // check items exists in cart
            if (cartItem == null) 
            {
                await _db.Carts.AddAsync(cart);
            }
            else
            {
                if (cart.Quantity < 1)
                {
                    // remove
                    await RemoveAsync(cartItem.Id);
                }
                else
                {
                    cartItem.Quantity = cart.Quantity;
                    cartItem.UpdatedDate = DateTime.UtcNow;
                    _db.Carts.Update(cartItem);
                }
            }
            await SaveAsync();
        }

        public async Task<bool> RemoveCartItemAsync(string userId, int cartItemId)
        {
            var cartHeader = await FetchAsync(ch => ch.UserId == userId);
            if (cartHeader != null)
            {
                var cartItems = await _db.Carts.Where(ci => ci.CartHeaderId == cartHeader.Id).ToListAsync();
                var count = cartItems.Count;
                //var cartItem = await _db.Carts.Where(ci => ci.CartHeaderId == cartHeader.Id && ci.Id == cartItemId).FirstOrDefaultAsync();
                if (cartItems != null)
                {
                    foreach (var cartItem in cartItems)
                    {
                        if (cartItem.Id == cartItemId) 
                        {
                            // remove cart items
                            _db.Carts.Remove(cartItem);
                            await SaveAsync();

                            if (count < 2) 
                            {
                                // remove cartheader
                                _db.CartHeaders.Remove(cartHeader);
                                await SaveAsync();
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}