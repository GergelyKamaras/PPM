using PPMAPIDTOModelLibrary.SharedDTOs;

namespace PPMDTOModelLibrary.InputDTOs.Properties;

public interface IPropertyInputDTO
{
    string Name { get; set; }
    float Size { get; set; }
    decimal PurchasePrice { get; set; }
    DateTime PurchaseDate { get; set; }
    string OwnerId { get; set; }
    public AddressDTO Address { get; set; }
    public bool IsRental { get; set; }

}