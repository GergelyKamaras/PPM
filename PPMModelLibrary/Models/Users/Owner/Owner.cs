using System.ComponentModel.DataAnnotations;

namespace AuthServiceModelLibrary.ApplicationUser.Owner
{
    public class Owner : IOwner
    {
        [Key]
        public int Id { get; set; }
    }
}
