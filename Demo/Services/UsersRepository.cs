using Demo.Data;
using Demo.Entities;
using Demo.Models.Requests;
using Demo.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly AuthDbContext _context;

        public UsersRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenRepository tokenRepository, AuthDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _context = context;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return result;

            var userProfile = new UserProfile
            {
                UserId = user.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

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

        public async Task<List<UserProfile>> GetAllUsers()
        {
            try
            {
                var users = await _context.UserProfiles
                    .Select(u => new UserProfile
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    })
                    .ToListAsync();
                return users;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return new List<UserProfile>();
            }
        }
    }
}
