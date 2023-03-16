using System.ComponentModel.DataAnnotations;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMAPIModelLibrary.Users;
using PPMAPIModelLibrary.UtilityModels;

namespace PPMAPIModelLibrary.Properties
{
    public class Property : IProperty
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Owner Owner { get; set; }
        public List<Cost> Costs { get; set; } = new List<Cost>();
        public List<Revenue> Revenues { get; set; } = new List<Revenue>();
        public List<ValueIncrease> ValueIncreases { get; set; } = new List<ValueIncrease>();
        public List<ValueDecrease> ValueDecreases { get; set; } = new List<ValueDecrease>();
    }
}
