using PPMModelLibrary.Models.FinancialObjects.Transactions;
using PPMModelLibrary.Models.FinancialObjects.ValueModifiers;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;
using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.Properties
{
    public class RentalProperty : IRentalProperty
    {
        [Key]
        public Guid Id { get; set; }
        public Tenant? Tenant { get; set; }
        public decimal? RentalFee { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Owner Owner { get; set; }
        public List<Cost> Costs { get; set; }
        public List<Revenue> Revenues { get; set; }
        public List<ValueIncrease> ValueIncreases { get; set; }
        public List<ValueDecrease> ValueDecreases { get; set; }
    }
}
