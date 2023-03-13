using PPMModelLibrary.Models.Properties;
using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.ValueModifiers
{
    public class ValueIncrease : IValueChange
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public Property? Property { get; set; }
        public RentalProperty? RentalProperty { get; set; }
    }
}
