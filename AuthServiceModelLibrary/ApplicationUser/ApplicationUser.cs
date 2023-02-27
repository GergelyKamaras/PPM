using System.ComponentModel.DataAnnotations;
using AuthServiceModelLibrary.Enums;
using Microsoft.AspNetCore.Identity;

namespace AuthServiceModelLibrary.ApplicationUser
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        [Required]
        public override string Email { get; set; }
        [Required]
        public override string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salt { get; set; }
        [Required]
        public override string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
