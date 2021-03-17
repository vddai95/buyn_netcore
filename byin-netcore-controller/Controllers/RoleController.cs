using System.Collections.Generic;
using System.Threading.Tasks;
using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace byin_netcore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleBusiness _roleBusiness;
        public RoleController(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRoles([FromBody] List<string> roles)
        {
            var rolesObj = new List<Role>();
            foreach(var role in roles)
            {
                var roleObj = new Role
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };
                rolesObj.Add(roleObj);
            }

            await _roleBusiness.AddRolesAsync(rolesObj).ConfigureAwait(false);

            return Ok();
        }

        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleBusiness.GetAllRoles().ConfigureAwait(false);

            return Ok(result);
        }
    }
}
