using System.ComponentModel.DataAnnotations;
using PPMAPIModelLibrary.Properties;

namespace PPMAPIModelLibrary.Users
{
    public class Owner : IUser
    {
        [Key]
        public string UserId { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();
        public List<RentalProperty> RentalProperties { get; set; } = new List<RentalProperty>();
    }
}
