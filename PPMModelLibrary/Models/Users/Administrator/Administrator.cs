using System.ComponentModel.DataAnnotations;

namespace AuthServiceModelLibrary.ApplicationUser.Administrator
{
    public class Administrator : IAdministrator
    {
        [Key]
        public int Id { get; set; }
    }
}
