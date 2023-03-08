using PPMModelLibrary.Models.Users;

namespace PPMModelLibrary.Models.Properties
{
    internal interface IRentable
    {
        public Tenant Tenant { get; set; }
        public decimal RentalFee { get; set; }
    }
}
