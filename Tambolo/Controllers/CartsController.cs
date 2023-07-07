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

                _response.Data = _mapper.Map<CartResponse>(carts);
                _response.Message = new List<string> { "Carts Retrieved Successfully!" };
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

                _response.Data = _mapper.Map<CartResponse>(cart);
                _response.Message = new List<string> { "Cart Retrieved Successfully!" };
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
                _response.Message = new List<string> { "Cart Created Successfully!" };
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
                await _cartRepository.CreateAsync(cart);

                _response.Status = HttpStatusCode.Created;
                _response.Data = _mapper.Map<CartResponse>(cart);
                _response.Message = new List<string> { "Cart Created Successfully!" };
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
