using ChatAppAPI.Services.Interfaces;
using ChatAppContext.Context;
using ChatAppContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Services
{
    public class TokenService : ITokenService
    {
        private ChatAppDBContext _dbContext;
        public TokenService(ChatAppDBContext dBContext) 
        { 
            this._dbContext = dBContext;
        }
        public async Task<bool> DeleteToken(User user)
        {
            var tokenEntity = await this._dbContext.Tokens.Where(t => t.UserId == user.UserId).FirstOrDefaultAsync();
            if (tokenEntity != null)
            {
                this._dbContext.Tokens.Remove(tokenEntity);
                this._dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> HasToken(User user)
        {
            return await this._dbContext.Tokens.AnyAsync(t => t.UserId == user.UserId);
        }

        public async Task<Token> GetToken(User user)
        {
            var tokenEntity = new Token();
            tokenEntity.UserId = user.UserId;
            tokenEntity.IssueDate = DateTime.Now;
            await this._dbContext.AddAsync(tokenEntity);
            await this._dbContext.SaveChangesAsync();
            return await this._dbContext.Tokens.Where(t => t.UserId == user.UserId).FirstOrDefaultAsync();
        }
    }
}
