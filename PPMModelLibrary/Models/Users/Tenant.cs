using System.ComponentModel.DataAnnotations;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Users;

namespace PPMModelLibrary.Models.Users
{
    public class Tenant : IUser
    {
        [Key]
        public string UserId { get; set; }
        private IRentable Property { get; set; }
    }
}
