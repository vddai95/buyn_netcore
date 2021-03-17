using System;
using System.Security.Claims;

namespace byin_netcore_business.Interfaces
{
    public interface IApplicationUser
    {
        Guid Id { get; }
        string Name { get; }
        ClaimsPrincipal CurrentUser { get; }
        string ClientIp { get; }
    }
}
