using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tambolo.Dtos;
using Tambolo.Models;
using Tambolo.Repositories;
using Tambolo.Repositories.IRepository;
using static Azure.Core.HttpHeader;

namespace Tambolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICouponRepository _couponRepository;
        private TamboloResponse _response;

        public CouponsController(IMapper mapper, ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
            _response = new TamboloResponse();
        }

        [HttpGet]
        public async Task<ActionResult<TamboloResponse>> Get() 
        {
            try
            {
                IEnumerable<Coupon> coupons = await _couponRepository.FetchAllAsync();

                _response.Data = _mapper.Map<IEnumerable<CouponResponse>>(coupons);
                _response.Message = new List<string> { "Coupons retrieved successfully!" };
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

        [HttpGet("{couponId:int}", Name = "GetCouponById")]
        public async Task<ActionResult<TamboloResponse>> Get([FromRoute] int couponId) 
        {
            try
            {
                Coupon coupon = await _couponRepository.FetchAsync(c => c.Id == couponId);

                if (coupon == null)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = new List<string> { "Coupon not found!" };
                    return NotFound(_response);
                }

                _response.Data = _mapper.Map<CouponResponse>(coupon);
                _response.Message = new List<string> { "Coupon retrieved successfully!" };
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
        public async Task<ActionResult<TamboloResponse>> Post([FromBody] CouponRequest request)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(request);
                await _couponRepository.CreateAsync(coupon);

                _response.Status = HttpStatusCode.Created;
                _response.Data = _mapper.Map<CouponResponse>(coupon);
                _response.Message = new List<string> { "Coupon created successfully!" };
                return CreatedAtRoute("GetCouponById", new { couponId = coupon.Id }, _response);
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
        public async Task<ActionResult<TamboloResponse>> Put([FromBody] CouponUpdateRequest request)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(request);
                await _couponRepository.UpdateAsync(coupon);

                _response.Status = HttpStatusCode.Created;
                _response.Message = new List<string> { "Coupon updated successfully!" };
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
