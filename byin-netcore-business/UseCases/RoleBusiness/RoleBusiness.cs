using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using byin_netcore_business.UseCases.Base;
using byin_netcore_business.UseCases.RoleBusiness.Authorization;
using byin_netcore_transver.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace byin_netcore_business.UseCases.RoleBusiness
{
    public class RoleBusiness : BaseBusiness, IRoleBusiness
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleBusiness(RoleManager<IdentityRole> roleManager, IAuthorizationBusiness authorizationService) : base(authorizationService)
        {
            _roleManager = roleManager;
        }

        public async Task AddRolesAsync(List<Role> roles)
        {
            foreach(var role in roles)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(role, RoleOperations.Create).ConfigureAwait(false);
                if (!isAuthorized.Succeeded)
                {
                    throw new HttpUnAuthorizedException();
                }
            }

            foreach(var role in roles)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role.Name,
                    NormalizedName = role.NormalizedName ?? role.Name.ToUpper()
                }).ConfigureAwait(false);
            }
        }

        public async Task<List<string>> GetAllRoles()
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(new Role(), RoleOperations.ReadAll).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                throw new HttpUnAuthorizedException();
            }

            return _roleManager.Roles.Select(r => r.Name).ToList();
        }
    }
}
