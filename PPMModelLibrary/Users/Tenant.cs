using System.ComponentModel.DataAnnotations;
using PPMAPIModelLibrary.Properties;

namespace PPMAPIModelLibrary.Users
{
    public class Tenant : IUser
    {
        [Key]
        public string UserId { get; set; }
        public List<RentalProperty> Properties { get; set; } = new List<RentalProperty>();
    }
}
