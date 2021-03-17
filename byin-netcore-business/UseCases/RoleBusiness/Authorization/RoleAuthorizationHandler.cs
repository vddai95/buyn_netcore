using byin_netcore_business.Constants;
using byin_netcore_business.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.RoleBusiness.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Role>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Role roleResource)
        {
            if(context.User is null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(RoleNames.ADMIN))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
