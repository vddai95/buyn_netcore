using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace byin_netcore.RequestModel
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<string> Roles { get; set; }
    }
}
