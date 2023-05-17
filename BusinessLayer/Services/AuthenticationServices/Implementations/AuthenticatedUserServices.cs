using BusinessLayer.Services.AuthenticationServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class AuthenticatedUserServices : IAuthenticatedUserServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticatedUserServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetAuthenticatedUserId()
        {
            var userId = 0;
            if (httpContextAccessor.HttpContext != null)
            {
                var NameIdentifier = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                userId = Int32.Parse(NameIdentifier);
            }
            return userId;
        }
    }
}
