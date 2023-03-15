namespace PPMDTOModelLibrary.InputDTOs.Properties;

public interface IPropertyInputDTO
{
    string Name { get; set; }
    float Size { get; set; }
    decimal PurchasePrice { get; set; }
    DateTime PurchaseDate { get; set; }
    string OwnerId { get; set; }
    string Country { get; set; }
    string City { get; set; }
    string ZipCode { get; set; }
    string Street { get; set; }
    int StreetNumber { get; set; }
    string? AdditionalInfo { get; set; }
}