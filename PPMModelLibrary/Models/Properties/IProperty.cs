using PPMModelLibrary.Models.FinancialObjects.Transactions;
using PPMModelLibrary.Models.FinancialObjects.ValueModifiers;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMModelLibrary.Models.Properties
{
    internal interface IProperty
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Address Address { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public Owner Owner { get; set; }
        public List<Cost> Costs { get; set; }
        public List<Revenue> Revenues { get; set; }
        public List<ValueIncrease> ValueIncreases { get; set; }
        public List<ValueDecrease> ValueDecreases { get; set; }
    }
}
