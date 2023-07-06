using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tambolo.Dtos;
using Tambolo.Models;
using Tambolo.Repositories;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private TamboloResponse _response;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _response = new TamboloResponse();
        }

        [HttpGet]
        public async Task<ActionResult<TamboloResponse>> Get() 
        {
            try
            {
                IEnumerable<Order> orders = await _orderRepository.FetchAllAsync();

                _response.Status = HttpStatusCode.OK;
                _response.Data = _mapper.Map<IEnumerable<OrderResponse>>(orders);
                _response.Message = new List<string> { "Orders fetched successfully!" };
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

        [HttpGet("{orderId:int}", Name = "GetOrderById")]
        public async Task<ActionResult<TamboloResponse>> Get([FromRoute] int orderId) 
        {
            try
            {
                Order order = await _orderRepository.FetchAsync(orderId);

                if (order == null)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.Message = new List<string> { "Order not found!" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.OK;
                _response.Data = _mapper.Map<OrderResponse>(order);
                _response.Message = new List<string> { "Order fetched successfully!" };
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

        [HttpPost]
        public async Task<ActionResult<TamboloResponse>> Post([FromBody] OrderRequest request) 
        {
            try
            {
                Order order = _mapper.Map<Order>(request);
                await _orderRepository.CreateAsync(order);

                _response.Status = HttpStatusCode.Created;
                _response.Message = new List<string> { "Order created successfully!" };
                return CreatedAtRoute("GetOrderById", new { orderId = order.Id }, _response);
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
        public async Task<ActionResult<TamboloResponse>> Put([FromBody] OrderUpdateRequest request) 
        {
            try
            {
                Order order = _mapper.Map<Order>(request);
                await _orderRepository.UpdateAsync(order);

                _response.Status = HttpStatusCode.Created;
                _response.Message = new List<string> { "Order updated successfully!" };
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
