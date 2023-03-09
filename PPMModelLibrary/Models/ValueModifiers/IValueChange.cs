using PPMModelLibrary.Models.Properties;

namespace PPMModelLibrary.Models.ValueModifiers
{
    internal interface IValueChange
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public Property? Property { get; set; }
        public RentableProperty? RentableProperty { get; set; }
    }
}
