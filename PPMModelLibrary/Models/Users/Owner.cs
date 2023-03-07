using System.ComponentModel.DataAnnotations;
using AuthServiceModelLibrary.ApplicationUser;

namespace PPMModelLibrary.Models.Users
{
    public class Owner : IUser
    {
        [Key]
        public string UserId { get; set; }

        private List<IProperty> Properties { get; set; }
    }
}
