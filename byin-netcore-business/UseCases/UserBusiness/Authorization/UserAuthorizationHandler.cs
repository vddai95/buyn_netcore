using byin_netcore_business.Constants;
using byin_netcore_business.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.UserBusiness.Authorization
{
    public class UserAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, User userResource)
        {
            if (userResource is null)
            {
                return Task.CompletedTask;
            }

            if(context.User is null && requirement.Name == OperationNames.Create && userResource.Roles.Count == 1 && userResource.Roles.Contains(RoleNames.CUSTOMER))
            {
                context.Succeed(requirement);
            }

            if(context.User is null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(RoleNames.ADMIN) || context.User.IsInRole(RoleNames.DEVELOPER) || context.User.IsInRole(RoleNames.ACCOUNT_MANAGER))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
