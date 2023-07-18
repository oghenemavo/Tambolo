using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Tambolo.Dtos;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private TamboloResponse _response;

        public CartsController(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _response = new TamboloResponse();
        }

        [HttpGet]
        public async Task<ActionResult<TamboloResponse>> Get()
        {
            try
            {
                //string authHeader = Request.Headers["Authorization"];
                //authHeader = authHeader.Replace("Bearer ", "");

                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(authHeader);
                //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                //var userId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var userId = "10a0d54f-5738-4535-a830-e9339a71b8ed";

                IEnumerable<Cart> cart = await _cartRepository.FetchAllAsync(c => c.UserId == userId);

                _response.Data = _mapper.Map<IEnumerable<CartResponse>>(cart);
                _response.Message = new List<string> { "Carts retrieved successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        //[HttpGet("{cartId:int}", Name = "GetCartById")]
        //public async Task<ActionResult<TamboloResponse>> Get(int cartId)
        //{
        //    try
        //    {
        //        Cart cart = await _cartRepository.FetchAsync(c => c.Id == cartId);

        //        if (cart == null)
        //        {
        //            _response.Status = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            _response.Message = new List<string> { "Cart not found" };
        //            return NotFound(_response);
        //        }

        //        _response.Data = _mapper.Map<CartResponse>(cart);
        //        _response.Message = new List<string> { "Cart retrieved successfully!" };
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Status = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.Message = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}

        //[Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<TamboloResponse>> Post(CartRequest model)
        {
            try
            {
                //string authHeader = Request.Headers["Authorization"];
                //authHeader = authHeader.Replace("Bearer ", "");

                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(authHeader);
                //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                //var userId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var userId = "10a0d54f-5738-4535-a830-e9339a71b8ed";
                model.UserId = userId;

                var cartItem = _mapper.Map<Cart>(model);
                // add to cart
                await _cartRepository.AddToCartAsync(cartItem);

                _response.Status = HttpStatusCode.Created;
                //_response.Data = null;
                _response.Data = _mapper.Map<CartResponse>(model);
                _response.Message = new List<string> { "Cart created successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        //[HttpPut]
        //public async Task<ActionResult<TamboloResponse>> Put(CartUpdateRequest request)
        //{
        //    try
        //    {
        //        Cart cart = _mapper.Map<Cart>(request);
        //        await _cartRepository.UpdateAsync(cart);

        //        _response.Status = HttpStatusCode.NoContent;
        //        _response.Message = new List<string> { "Cart updated successfully!" };
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Status = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.Message = new List<string> { ex.Message };
        //    }
        //    return _response;
        //}

        //[Authorize(Roles = "User")]
        [HttpDelete("{cartItemId:int}")]
        public async Task<ActionResult<TamboloResponse>> Delete(int cartItemId)
        {
            try
            {
                //string authHeader = Request.Headers["Authorization"];
                //authHeader = authHeader.Replace("Bearer ", "");

                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(authHeader);
                //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                //var userId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var userId = "10a0d54f-5738-4535-a830-e9339a71b8ed";

                bool isDeleted = await _cartRepository.RemoveAsync(userId, cartItemId);

                if (!isDeleted)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = new List<string> { "Cart Item not found" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Cart Item deleted successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<ActionResult<TamboloResponse>> Delete()
        {
            try
            {
                string authHeader = Request.Headers["Authorization"];
                authHeader = authHeader.Replace("Bearer ", "");

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                var userId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                bool isDeleted = await _cartRepository.EmptyCartAsync(userId);

                if (!isDeleted)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = new List<string> { "Cart not found" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Cart deleted successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        //[Authorize(Roles = "User")]
        [HttpPost("ApplyCoupon/{code}")]
        public async Task<ActionResult<TamboloResponse>> ApplyCoupon(string code)
        {
            try
            {
                //string authHeader = Request.Headers["Authorization"];
                //authHeader = authHeader.Replace("Bearer ", "");

                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(authHeader);
                //var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                //var userId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var userId = "10a0d54f-5738-4535-a830-e9339a71b8ed";

                bool isDeleted = await _cartRepository.ApplyCouponAsync(userId, code);

                if (!isDeleted)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = new List<string> { "Cart Item not found" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Cart Item deleted successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
