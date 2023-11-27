using System.ComponentModel.DataAnnotations;

namespace Microservice.Authentication.Models
{
    public class UserLogin
    {
        
        [EmailAddress]

        public string Email { get; set; }

        [Required]

        public string Password { get; set; }
    }
}
