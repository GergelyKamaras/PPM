using System.ComponentModel.DataAnnotations;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Users;

namespace PPMModelLibrary.Models.Users
{
    public class Owner : IUser
    {
        [Key]
        public string UserId { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();
        public List<RentalProperty> RentalProperties { get; set; } = new List<RentalProperty>();
    }
}
