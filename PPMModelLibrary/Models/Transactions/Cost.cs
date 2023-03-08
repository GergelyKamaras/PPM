namespace PPMModelLibrary.Models.Transactions
{
    public class Cost : ITransaction
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
