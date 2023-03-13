using System.ComponentModel.DataAnnotations;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Users;

namespace PPMModelLibrary.Models.Users
{
    public class Tenant : IUser
    {
        [Key]
        public string UserId { get; set; }
        public List<RentalProperty> Properties { get; set; } = new List<RentalProperty>();
    }
}
