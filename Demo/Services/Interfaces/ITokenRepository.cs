using Demo.Data;

namespace Demo.Services.Interfaces
{
    public interface ITokenRepository
    {
        public string CreateToken(ApplicationUser user);
    }
}
