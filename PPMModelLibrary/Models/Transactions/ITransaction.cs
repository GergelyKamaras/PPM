namespace PPMModelLibrary.Models.Transactions
{
    internal interface ITransaction
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
