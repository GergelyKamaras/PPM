using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityModelLibrary.Enums;
using Microsoft.AspNetCore.Identity;

namespace EntityModelLibrary.Models.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salt { get; set; }
        public UserRole Role { get; set; }
    }
}
