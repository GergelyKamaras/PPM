using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.Users.Tenant
{
    public class Tenant : ITenant
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }
}
