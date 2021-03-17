using byin_netcore_business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.AuthorizationBusiness
{
    public class AuthorizationBusiness : DefaultAuthorizationService, IAuthorizationBusiness
    {
        private readonly IApplicationUser _applicationUser;
        public AuthorizationBusiness(IAuthorizationPolicyProvider policyProvider, IAuthorizationHandlerProvider handlers, ILogger<DefaultAuthorizationService> logger, IAuthorizationHandlerContextFactory contextFactory, IAuthorizationEvaluator evaluator, IOptions<AuthorizationOptions> options, IApplicationUser applicationUser) : base(policyProvider, handlers, logger, contextFactory, evaluator, options)
        {
            _applicationUser = applicationUser;
        }

        public async Task<AuthorizationResult> AuthorizeAsync(object resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            return await this.AuthorizeAsync(_applicationUser.CurrentUser, resource, requirements).ConfigureAwait(false);
        }

        public async Task<AuthorizationResult> AuthorizeAsync(object resource, string policyName)
        {
            return await this.AuthorizeAsync(_applicationUser.CurrentUser, resource, policyName).ConfigureAwait(false);
        }

        public async Task<AuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy)
        {
            return await this.AuthorizeAsync(_applicationUser.CurrentUser, policy).ConfigureAwait(false);
        }

        public async Task<AuthorizationResult> AuthorizeAsync(object resource, AuthorizationPolicy policy)
        {
            return await this.AuthorizeAsync(_applicationUser.CurrentUser, resource, policy).ConfigureAwait(false);
        }

        public async Task<AuthorizationResult> AuthorizeAsync(object resource, IAuthorizationRequirement requirement)
        {
            return await this.AuthorizeAsync(_applicationUser.CurrentUser, resource, requirement).ConfigureAwait(false);
        }

        public async Task<AuthorizationResult> AuthorizeAsync(string policyName)
        {
            return await this.AuthorizeAsync(_applicationUser.CurrentUser, policyName).ConfigureAwait(false);
        }
    }
}
