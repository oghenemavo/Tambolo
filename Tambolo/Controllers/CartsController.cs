using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                IEnumerable<Cart> carts = await _cartRepository.FetchAllAsync();

                _response.Data = _mapper.Map<IEnumerable<CartResponse>>(carts);
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

        [HttpGet("{cartId:int}", Name = "GetCartById")]
        public async Task<ActionResult<TamboloResponse>> Get(int cartId)
        {
            try
            {
                Cart cart = await _cartRepository.FetchAsync(c => c.Id == cartId);

                if (cart == null)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = new List<string> { "Cart not found" };
                    return NotFound(_response);
                }

                _response.Data = _mapper.Map<CartResponse>(cart);
                _response.Message = new List<string> { "Cart retrieved successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<TamboloResponse>> Post(CartRequest request)
        {
            try
            {
                Cart cart = _mapper.Map<Cart>(request);
                await _cartRepository.CreateAsync(cart);

                _response.Status = HttpStatusCode.Created;
                _response.Data = _mapper.Map<CartResponse>(cart);
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

        [HttpPut]
        public async Task<ActionResult<TamboloResponse>> Put(CartUpdateRequest request)
        {
            try
            {
                Cart cart = _mapper.Map<Cart>(request);
                await _cartRepository.UpdateAsync(cart);

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Cart updated successfully!" };
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{cartId:int}")]
        public async Task<ActionResult<TamboloResponse>> Delete(int cartId)
        {
            try
            {
                bool isDeleted = await _cartRepository.RemoveAsync(cartId);

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
    }
}
