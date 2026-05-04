using ReciclajeApp.Domain.Entities;

namespace ReciclajeApp.Application.Auth;

public interface IAuthService{
    Task<string> Register(User user);
    Task<string> Login(string email, string password);
    Task<string> Logout(string token);
    Task<string> RefreshToken(string token);
}
