using System.ComponentModel.DataAnnotations;

namespace AuthServiceModelLibrary.ApplicationUser.Tenant
{
    public class Tenant : ITenant
    {
        [Key]
        public int Id { get; set; }
    }
}
