using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMModelLibrary.Models.Properties
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
        public List<Cost> Costs { get; set; }
        public List<Revenue> Revenues { get; set; }
        public List<ValueIncrease> ValueIncreases { get; set; }
        public List<ValueDecrease> ValueDecreases { get; set; }
    }
}
