using byin_netcore_business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using IdentityModel;
using System.Security.Claims;

namespace byin_netcore_business.UseCases.UserBusiness
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid Id => this.GetUserId();

        public string Name => this.GetUserName();

        public ClaimsPrincipal CurrentUser => GetCurrentUser();

        public string ClientIp => GetClientIp();

        private string GetClientIp()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        private Guid GetUserId()
        {
            var subject = _httpContextAccessor.HttpContext
                              .User.Claims
                              .FirstOrDefault(claim => claim.Type == JwtClaimTypes.Subject);

            return Guid.TryParse(subject.Value, out var id) ? id : Guid.Empty;
        }

        private string GetUserName()
        {
            return _httpContextAccessor.HttpContext
                              .User.Claims
                              .FirstOrDefault(claim => claim.Type == JwtClaimTypes.PreferredUserName).Value;
        }

        private ClaimsPrincipal GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext.User;
        }
    }
}
