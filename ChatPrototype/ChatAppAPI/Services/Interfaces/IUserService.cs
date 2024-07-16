using ChatAppContext.DTO;
using ChatAppContext.EntityVM;

namespace ChatAppAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Login(string username, string password);

        Task<UserVM> Register(UserVM newUser);

        Task<bool> VerifyToken(string token);

        Task<int> GetUserIdByUsername(string username);
    }
}
