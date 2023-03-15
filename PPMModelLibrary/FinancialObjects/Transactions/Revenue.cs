using System.ComponentModel.DataAnnotations;
using PPMAPIModelLibrary.Properties;

namespace PPMAPIModelLibrary.FinancialObjects.Transactions
{
    public class Revenue : ITransaction
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public Property? Property { get; set; }
        public RentalProperty? RentalProperty { get; set; }
    }
}
