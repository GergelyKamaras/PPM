using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMModelLibrary.Models.Properties
{
    internal class Property : IProperty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public Owner Owner { get; set; }
        public List<Cost> Costs { get; set; }
        public List<Income> Incomes { get; set; }
        public List<ValueIncrease> ValueIncreases { get; set; }
        public List<ValueDecrease> ValueDecreases { get; set; }

        public Property(decimal price, DateTime date, Address address, Owner owner)
        {
            Address = address;
            Owner = owner;
            PurchasePrice = price;
            PurchaseDate = date;
        }
    }
}
