using Demo.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Demo.Services.Interfaces
{
    public interface IUsersRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterUserRequest request);
        Task<string?> LoginAsync(LoginRequest request);
    }
}
