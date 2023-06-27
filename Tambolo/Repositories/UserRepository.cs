using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tambolo.Data;
using Tambolo.Dtos;
using Tambolo.Models;
using Tambolo.Repositories.IRepository;

namespace Tambolo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IConfiguration _config;

        public UserRepository(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<bool> CreateAccountAsync(UserCreateDto model)
        {
            ApplicationUser user = new ApplicationUser 
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                NormalizedEmail = model.Email.ToUpper(),
                NormalizedUserName = model.Username.ToUpper()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Super Admin");
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto model)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());

            var response = new LoginResponseDto();

            bool isValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (user == null || !isValid)
            {
                return response;
            }
            
            var roles = await _userManager.GetRolesAsync(user);

            var secretKey = _config.GetValue<string>("AuthSettings:Secret") ?? "";
            var tokenHandler = new JwtSecurityTokenHandler();
            // get secret in bytes
            var key = Encoding.ASCII.GetBytes(secretKey);
            // get claims
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.UserData, user.FirstName + "" + user.LastName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            // create token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.token = tokenHandler.WriteToken(token);

            return response;
        }
    }
}
