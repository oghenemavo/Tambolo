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

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserCreateDto model)
        {
            var result = await _userRepository.CreateAccountAsync(model);
            if (result)
            {
                return Ok(1);
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _userRepository.LoginAsync(model);
            if (string.IsNullOrEmpty(result.token))
            {
                return BadRequest("Invalid Username or Password");
            }
            return Ok(result.token);
        }
    }
}
