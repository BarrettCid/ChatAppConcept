using ChatAppContext.Entities;

namespace ChatAppAPI.Services.Interfaces
{
    public interface ITokenService
    {
        Task<Token> GetToken(User user);

        Task<bool> HasToken(User user);

        Task<bool> DeleteToken(User user);
    }
}
