using PPMModelLibrary.Models.Users;

namespace PPMModelLibrary.Models.Properties
{
    internal interface IRental
    {
        public Tenant? Tenant { get; set; }
        public decimal? RentalFee { get; set; }
    }
}
