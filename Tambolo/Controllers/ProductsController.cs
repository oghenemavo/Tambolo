using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Tambolo.Dtos;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private TamboloResponse _response;
        public ProductsController(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _response = new TamboloResponse();
        }

        [HttpGet]
        public async Task<ActionResult<TamboloResponse>> Get()
        {
            try
            {
                IEnumerable<Product> products = await _productRepository.FetchAllAsync();

                _response.Status = HttpStatusCode.OK;
                _response.Data = _mapper.Map<IEnumerable<ProductResponse>>(products);
                _response.Message = new List<string> { "Products fetched successfully!" };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("{productId:int}", Name = "GetProductById")]
        public async Task<ActionResult<TamboloResponse>> Get([FromRoute] int productId)
        {
            try
            {
                Product product = await _productRepository.FetchAsync(p => p.Id == productId);

                if (product == null)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.Message = new List<string> { "Product not found!" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.OK;
                _response.Data = _mapper.Map<ProductResponse>(product);
                _response.Message = new List<string> { "Product fetched successfully!" };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [Authorize(Roles = "Vendor")]
        [HttpPost]
        public async Task<ActionResult<TamboloResponse>> Post([FromBody] ProductRequest model)
        {
            try
            {
                string authHeader = Request.Headers["Authorization"];
                authHeader = authHeader.Replace("Bearer ", "");

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(authHeader);
                var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
                var userId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                // set user id
                model.UserId = userId;
                var product = _mapper.Map<Product>(model);
                await _productRepository.CreateAsync(product);

                _response.Status = HttpStatusCode.Created;
                _response.Data = _mapper.Map<ProductResponse>(product);
                _response.Message = new List<string> { "Product created successfully!" };
                return CreatedAtRoute("GetProductById", new { productId = product.Id }, _response);
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
        [Authorize(Roles = "Vendor")]
        public async Task<ActionResult<TamboloResponse>> Put([FromBody] ProductUpdateRequest request)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                await _productRepository.UpdateAsync(product);

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Product updated successfully!" };
                return CreatedAtRoute("GetProductById", new { productId = product.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{productId:int}")]
        [Authorize(Roles = "Vendor")]
        public async Task<ActionResult<TamboloResponse>> Delete([FromRoute] int productId)
        {
            try
            {
                bool isRemoved = await _productRepository.RemoveAsync(productId);

                if (!isRemoved)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.Message = new List<string> { "Product not found!" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Product deleted successfully!" };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("status")]
        public async Task<ActionResult<TamboloResponse>> GetProductStatus()
        {
            try
            {
                var statuses = await _productRepository.FetchProductStatuses();

                _response.Status = HttpStatusCode.NoContent;
                _response.Data = statuses;
                _response.Message = new List<string> { "Products statuses fetched successfully!" };
                return Ok(_response);
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
