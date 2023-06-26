using Microsoft.AspNetCore.Identity;
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

        public UserRepository(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

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
    }
}
