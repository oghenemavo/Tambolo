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
    public class CategoriesController : ControllerBase
    {
        private TamboloResponse _response;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _response = new TamboloResponse();
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
         
        [HttpGet]
        public async Task<ActionResult<TamboloResponse>> Get()
        {
            try
            {
                var categories = await _categoryRepository.FetchAllAsync();
                _response.Data = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
                _response.Message = new List<string> { "Categories fetched successfully!" };

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

        [HttpGet("{categoryId:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<TamboloResponse>> Get(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.FetchAsync(categoryId);

                if (category == null)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.Message = new List<string> { "Category not found!" };
                    return NotFound(_response);
                }

                _response.Data = _mapper.Map<CategoryResponse>(category);
                _response.Message = new List<string> { "Category fetched successfully!" };
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
        public async Task<ActionResult<TamboloResponse>> Post(CategoryRequest request)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(request);
                await _categoryRepository.CreateAsync(categoryEntity);

                _response.Status = HttpStatusCode.Created;
                _response.Data = _mapper.Map<CategoryResponse>(categoryEntity);
                _response.Message = new List<string> { "Category created successfully!" };

                return CreatedAtRoute("GetCategoryById", new { CategoryId = categoryEntity.Id }, _response);
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
        public async Task<ActionResult<TamboloResponse>> Put(CategoryResponse request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);
                await _categoryRepository.UpdateAsync(category);

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Category updated successfully!" };
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

        [HttpDelete("{categoryId:int}")]
        public async Task<ActionResult<TamboloResponse>> Delete(int categoryId)
        {
            try
            {
                bool isDeleted = await _categoryRepository.RemoveAsync(categoryId);

                if (!isDeleted)
                {
                    _response.Status = HttpStatusCode.NotFound;
                    _response.Message = new List<string> { "Category not found!" };
                    return NotFound(_response);
                }

                _response.Status = HttpStatusCode.NoContent;
                _response.Message = new List<string> { "Category deleted successfully!" };
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
