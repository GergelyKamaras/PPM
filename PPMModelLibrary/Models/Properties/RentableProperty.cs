using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMModelLibrary.Models.Properties
{
    public class RentableProperty : IRentableProperty
    {
        public Tenant Tenant { get; set; }
        public decimal RentalFee { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public List<Cost> Costs { get; set; }
        public List<Income> Incomes { get; set; }
        public List<ValueIncrease> ValueIncreases { get; set; }
        public List<ValueDecrease> ValueDecreases { get; set; }
        public RentableProperty(decimal price, DateTime date, Address address)
        {
            Address = address;
            PurchasePrice = price;
            PurchaseDate = date;
        }
    }
}
