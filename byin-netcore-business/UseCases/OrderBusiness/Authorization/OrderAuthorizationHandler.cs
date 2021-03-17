using byin_netcore_business.Constants;
using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.OrderBusiness.Authorization
{
    class OrderAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Order>
    {
        IApplicationUser _applicationUser;
        public OrderAuthorizationHandler(IApplicationUser applicationUser)
        {
            _applicationUser = applicationUser;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Order orderResource)
        {
            if (orderResource != null && requirement.Name == OperationNames.Create)
            {
                context.Succeed(requirement);
            }

            if (_applicationUser.CurrentUser?.IsInRole(RoleNames.ADMIN) ?? false)
            {
                context.Succeed(requirement);
            }

            if(requirement.Name == OperationNames.Read || requirement.Name == OperationNames.Delete || requirement.Name == OperationNames.Update)
            {
                if(orderResource.CustomerId is null)
                {
                    if(orderResource.CustomerIpAdress == _applicationUser.ClientIp)
                    {
                        context.Succeed(requirement);
                    }
                }
                else if(orderResource.CustomerId.ToString() == _applicationUser.Id.ToString())
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
