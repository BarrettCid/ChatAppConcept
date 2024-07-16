using ChatAppAPI.Handlers.Requirements;
using ChatAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

namespace ChatAppAPI.Handlers
{
    public class TokenHandler : AuthorizationHandler<TokenRequirement>
    {
        IHttpContextAccessor _httpContextAccessor;
        IUserService _userService;
        public TokenHandler(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenRequirement requirement)
        {
            HttpContext httpContext = this._httpContextAccessor.HttpContext;
            var _bearer_token = httpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var result = this._userService.VerifyToken(_bearer_token).Result;
            if (result)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
