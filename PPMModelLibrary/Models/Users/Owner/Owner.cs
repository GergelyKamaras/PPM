using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.Users.Owner
{
    public class Owner : IOwner
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
