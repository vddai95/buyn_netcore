using byin_netcore_business.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace byin_netcore_business.Interfaces
{
    public interface IUserBusiness
    {
        Task<User> AuthenticateAsync(string userName, string passWord);
        Task<string> CreateUserAsync(User user);
        Task<bool> ConfirmEmailAsync(string email, string token);
    }
}
