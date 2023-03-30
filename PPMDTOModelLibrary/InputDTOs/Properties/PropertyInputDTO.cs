using PPMAPIDTOModelLibrary.SharedDTOs;

namespace PPMDTOModelLibrary.InputDTOs.Properties
{
    public class PropertyInputDTO
    {
        public string Name { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OwnerId { get; set; }
        public AddressDTO Address { get; set; }
        public bool IsRental { get; set; }
        public string? TenantId { get; set; }
        public decimal RentalFee { get; set; } = 0;
    }
}
