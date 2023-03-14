namespace PPMDTOModelLibrary.InputDTOs.Transactions
{
    internal class FinancialInputDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string PropertyId { get; set; }
        public bool IsRental { get; set; }
        public string InputType { get; set; }
    }
}
