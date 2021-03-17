using byin_netcore.RequestModel;
using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace byin_netcore_controller.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        public AuthController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest model)
        {
            var user = await _userBusiness.AuthenticateAsync(model.Username, model.Password).ConfigureAwait(false);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest model)
        {
            var newUser = new User
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password,
                Roles = model.Roles.ToList()
            };

            var emailConfirmationToken = await _userBusiness.CreateUserAsync(newUser).ConfigureAwait(false);

            if(emailConfirmationToken is null)
            {
                return BadRequest(new { message = "Registration failed" });
            }
            
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token = emailConfirmationToken, email = model.Email }, Request.Scheme);
            //var message = new Message(new string[] { model.Email }, "Confirmation email link", confirmationLink, null);
            //await _emailSender.SendEmailAsync(message);

            return Ok(confirmationLink);
        }

        [AllowAnonymous]
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email )
        {
            var succeeded = await _userBusiness.ConfirmEmailAsync(email, token).ConfigureAwait(false);
            if (!succeeded)
            {
                return BadRequest(new { message = "email confirmation failed" });
            }
            return Ok();
        }
    }
}