using System.ComponentModel.DataAnnotations;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Users;

namespace PPMModelLibrary.Models.Users
{
    public class Tenant : IUser
    {
        [Key]
        public string UserId { get; set; }
        public List<RentableProperty> Properties { get; set; } = new List<RentableProperty>();
    }
}
