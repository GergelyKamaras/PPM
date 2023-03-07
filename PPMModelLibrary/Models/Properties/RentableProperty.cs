using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMModelLibrary.Models.Properties
{
    internal class RentableProperty : IRentableProperty
    {
        public Tenant Tenant { get; set; }
        public decimal RentalFee { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public Owner Owner { get; set; }
        public RentableProperty(decimal price, DateTime date, Address address, Owner owner)
        {
            Address = address;
            Owner = owner;
            PurchasePrice = price;
            PurchaseDate = date;
        }
    }
}
