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
    }
}
