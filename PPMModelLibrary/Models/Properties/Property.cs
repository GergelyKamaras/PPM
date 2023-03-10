using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMModelLibrary.Models.Properties
{
    public class Property : IProperty
    {
        [Key]
        public Guid Id { get; set; }
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
