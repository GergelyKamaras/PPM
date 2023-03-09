using System.ComponentModel.DataAnnotations;

namespace PPMModelLibrary.Models.Transactions
{
    public class Cost : ITransaction
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
