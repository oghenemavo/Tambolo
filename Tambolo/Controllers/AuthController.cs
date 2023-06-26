using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tambolo.Dtos;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserCreateDto model)
        {
            var result = await _userRepository.CreateAccountAsync(model);
            if (result)
            {
                return Ok(1);
            }
            return BadRequest();
        }
    }
}
