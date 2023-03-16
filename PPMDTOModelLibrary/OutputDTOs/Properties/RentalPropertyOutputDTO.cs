using PPMAPIDTOModelLibrary.SharedDTOs;

namespace PPMAPIDTOModelLibrary.OutputDTOs.Properties
{
    public class RentalPropertyOutputDTO : IRentalPropertyOutputDTO
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public AddressDTO Address { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OwnerId { get; set; }
        public decimal RentalFee { get; set; }
        public string TenantId { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Balance { get; set; }
    }
}
