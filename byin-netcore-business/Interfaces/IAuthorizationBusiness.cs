using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IAuthorizationBusiness : IAuthorizationService
    {
        Task<AuthorizationResult> AuthorizeAsync(object resource, IEnumerable<IAuthorizationRequirement> requirements);
        Task<AuthorizationResult> AuthorizeAsync(object resource, string policyName);
        Task<AuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy);
        Task<AuthorizationResult> AuthorizeAsync(object resource, AuthorizationPolicy policy);
        Task<AuthorizationResult> AuthorizeAsync(object resource, IAuthorizationRequirement requirement);
        Task<AuthorizationResult> AuthorizeAsync(string policyName);
    }
}
