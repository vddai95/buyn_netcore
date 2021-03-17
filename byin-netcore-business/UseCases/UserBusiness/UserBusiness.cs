using byin_netcore_business.Entity;
using byin_netcore_business.Interfaces;
using byin_netcore_transver;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using byin_netcore_transver.Exception;
using byin_netcore_business.UseCases.Base;
using byin_netcore_business.Constants;
using byin_netcore_business.UseCases.UserBusiness.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace byin_netcore_business.UseCases.UserBusiness
{
    public class UserBusiness : BaseBusiness, IUserBusiness
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public UserBusiness(IOptions<AppSettings> appSettings, UserManager<IdentityUser> userManager, IAuthorizationBusiness authorizationService) : base(authorizationService)
        {
            _appSettings = appSettings.Value;
            _userManager = userManager;
        }

        public async Task<User> AuthenticateAsync(string email, string passWord)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if(identityUser == null)
            {
                return null;
            }
            
            var checkingPassResult = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, passWord);
            if(checkingPassResult == PasswordVerificationResult.Failed)
            {
                return null;
            }
            if(!await _userManager.IsEmailConfirmedAsync(identityUser))
            {
                return null;
            }

            var claims = new List<Claim>();
            var roles = new List<string>(await _userManager.GetRolesAsync(identityUser));
            roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
            claims.Add(new Claim(ClaimTypes.Email, identityUser.Email));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new User
            {
                Id = identityUser.Id,
                Email = identityUser.Email,
                Password = null,
                Roles = new List<string>(await _userManager.GetRolesAsync(identityUser)),
                Token = tokenHandler.WriteToken(token)
            };
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }

        public async Task<string> CreateUserAsync(User user)
        {
            var isAuthorized = await _authorizationService.AuthorizeAsync(user, UserOperations.Create).ConfigureAwait(false);
            if (!isAuthorized.Succeeded)
            {
                throw new HttpUnAuthorizedException();
            }

            var userDb = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(userDb, user.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                var errorMessage = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }
                throw new HttpRegistrationException(errorMessage.ToString());
            }

            foreach(var role in user.Roles)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(userDb, role).ConfigureAwait(false);
                if (!addRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(userDb).ConfigureAwait(false);
                    var errorMessage = new StringBuilder();
                    foreach (var error in addRoleResult.Errors)
                    {
                        errorMessage.AppendLine(error.Description);
                    }
                    throw new HttpRegistrationException(errorMessage.ToString());
                }
            }
            
            return await _userManager.GenerateEmailConfirmationTokenAsync(userDb).ConfigureAwait(false);
        } 
    }
}
