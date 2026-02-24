using Demo.Data;
using Demo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Demo.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenRepository _tokenRepository;

        public UsersRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
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

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                return null;

            // TODO: Generate JWT here
            var token = _tokenRepository.CreateToken(user);
            return token;
        }
    }
}
