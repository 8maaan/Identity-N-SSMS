using Demo.Data;
using Demo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Demo.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                FirstName = "Test",
                LastName = "Only",
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return result;

            await _userManager.AddToRoleAsync(user, "Member");

            return result;
        }
    }
}
