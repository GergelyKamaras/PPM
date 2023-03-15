using PPMAPIDTOModelLibrary.SharedDTOs;

namespace PPMDTOModelLibrary.InputDTOs.Properties
{
    public class PropertyInputDTO : IPropertyInputDTO
    {
        public string Name { get; set; }
        public float Size { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OwnerId { get; set; }
        public AddressDTO Address { get; set; }
    }
}
