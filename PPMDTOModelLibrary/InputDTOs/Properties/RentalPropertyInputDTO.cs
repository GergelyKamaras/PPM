namespace PPMDTOModelLibrary.InputDTOs.Properties
{
    internal class RentalPropertyInputDTO
    {
        public string OwnerId { get; set; }
        public string? TenantId { get; set; }
        public decimal? RentalFee { get; set; }
        public string Name { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; }
        public DateTime PurchaseDate { get; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
