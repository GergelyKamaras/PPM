using System.ComponentModel.DataAnnotations;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PPMModelLibrary.Models.Users
{
    public class Tenant : IUser
    {
        [Key]
        public string UserId { get; set; }

        private IRentable Property { get; set; }
    }
}
