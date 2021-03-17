using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace byin_netcore.RequestModel
{
    [DataContract(Name = "AuthenticateRequest")]
    public class AuthenticateRequest
    {
        [Required(ErrorMessage = "username is required")]
        [DataMember(Name = "username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
