using System.ComponentModel.DataAnnotations;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Users;

namespace PPMModelLibrary.Models.Users
{
    public class Owner : IUser
    {
        [Key]
        public string UserId { get; set; }

        private List<IProperty> Properties { get; set; }
    }
}
