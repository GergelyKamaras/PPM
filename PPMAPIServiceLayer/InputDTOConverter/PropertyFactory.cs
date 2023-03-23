using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.UtilityModels;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public class PropertyFactory : IPropertyFactory
    {
        public Property CreateProperty(PropertyInputDTO property)
        {
            return new Property()
                {
                    Id = Guid.NewGuid(),
                    Address = CreateAddress(property.Address),
                    Name = property.Name,
                    PurchaseDate = property.PurchaseDate,
                    PurchasePrice = property.PurchasePrice,
                    Size = property.Size
                };
            }

        public RentalProperty CreateRentalProperty(PropertyInputDTO property)
        {
            return new RentalProperty()
                {
                    Id = Guid.NewGuid(),
                    Address = CreateAddress(property.Address),
                    Name = property.Name,
                    PurchaseDate = property.PurchaseDate,
                    PurchasePrice = property.PurchasePrice,
                    RentalFee = property.RentalFee,
                    Size = property.Size
                };
        }

        private Address CreateAddress(AddressDTO address)
        {
            return new Address()
                {
                    Country = address.Country,
                    City = address.City,
                    Street = address.Street,
                    StreetNumber = address.StreetNumber,
                    ZipCode = address.ZipCode,
                    AdditionalInfo = address.AdditionalInfo
                };
            }
    }
}
