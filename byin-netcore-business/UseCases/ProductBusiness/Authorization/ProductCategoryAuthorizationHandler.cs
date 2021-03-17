using byin_netcore_business.Constants;
using byin_netcore_business.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.ProductBusiness.Authorization
{
    public class ProductCategoryAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ProductCategory>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ProductCategory resource)
        {
            if (requirement.Name == OperationNames.Read || requirement.Name == OperationNames.ReadAll)
            {
                context.Succeed(requirement);
            }

            if (context.User != null && context.User.IsInRole(RoleNames.ADMIN))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
