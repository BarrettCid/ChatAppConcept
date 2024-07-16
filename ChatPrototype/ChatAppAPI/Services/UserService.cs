using AutoMapper;
using ChatAppAPI.Helpers;
using ChatAppAPI.Services.Interfaces;
using ChatAppContext.Context;
using ChatAppContext.DTO;
using ChatAppContext.Entities;
using ChatAppContext.EntityVM;
using Microsoft.EntityFrameworkCore;

namespace ChatAppAPI.Services
{
    public class UserService : IUserService
    {
        private ChatAppDBContext _dbContext;
        private IMapper _mapper;
        private ITokenService _tokenService;
        public UserService(ChatAppDBContext context, IMapper mapper, ITokenService tokenService)
        {
            this._dbContext = context;
            this._mapper = mapper;
            this._tokenService = tokenService;
        }

        public async Task<LoginResponseDTO> Login(string username, string password)
        {
            if( await this._dbContext.Users.AnyAsync(x => x.EmailAddress == username))
            {
                var userEntity = await this._dbContext.Users.Where(x => x.EmailAddress == username).FirstOrDefaultAsync();
                if (userEntity != null)
                {
                    var decryptedCypher = EncryptDecrypt.Decrypt(userEntity.Password);
                    if (decryptedCypher == password)
                    {
                        //If the user already has a token we replace it for a new issue date
                        if(await this._tokenService.HasToken(userEntity))
                        {
                            await this._tokenService.DeleteToken(userEntity);
                        }
                        var token = await this._tokenService.GetToken(userEntity);
                        LoginResponseDTO response = new LoginResponseDTO();
                        response.EmailAddress = userEntity.EmailAddress;
                        response.TokenId = token.TokenId;
                        response.IssueDate = token.IssueDate;
                        return response;
                    }
                }
            }
            throw new ArgumentException($"We were unable to find your username and password. Please try again.");    
        }

        public async Task<UserVM> Register(UserVM newUser)
        {
            var userEntity = this._mapper.Map<User>(newUser);
            if(!await this._dbContext.Users.AnyAsync(x => x.EmailAddress == userEntity.EmailAddress))
            {
                userEntity.Password = EncryptDecrypt.Encrypt(newUser.Password);
                await this._dbContext.Users.AddAsync(userEntity);
                await this._dbContext.SaveChangesAsync();
                return this._mapper.Map<UserVM>(await this._dbContext.Users.Where(x => x.EmailAddress == userEntity.EmailAddress).FirstOrDefaultAsync());
            }
            else
            {
                throw new ArgumentException($"The email {newUser.EmailAddress} is already registered. Please login.");   
            }
        }

        public async Task<bool> VerifyToken(string token)
        {
            Guid gToken = new Guid(token);
            var result = await this._dbContext.Users.Include(x => x.Token).Where(u => u.Token.TokenId == gToken).AnyAsync();
            return result;
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            return await this._dbContext.Users.Where(u => u.EmailAddress == username).Select(u => u.UserId).FirstOrDefaultAsync();
        }
    }
}
