using PPMAPIModelLibrary.Users;

namespace PPMAPIModelLibrary.Properties
{
    internal interface IRental
    {
        public Tenant? Tenant { get; set; }
        public decimal? RentalFee { get; set; }
    }
}
