using System;
using System.Collections.Generic;
using System.Text;

namespace byin_netcore_business.Entity
{
    public class User
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string PhoneNumber { get; set; }
    }
}
