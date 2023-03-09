using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;
using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.Properties
{
    public class RentableProperty : IRentableProperty
    {
        [Key]
        public Guid Id { get; set; }
        public Tenant? Tenant { get; set; }
        public decimal? RentalFee { get; set; }
        public string? Name { get; set; }
        public Address Address { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public List<Cost> Costs { get; set; }
        public List<Revenue> Revenues { get; set; }
        public List<ValueIncrease> ValueIncreases { get; set; }
        public List<ValueDecrease> ValueDecreases { get; set; }
    }
}
