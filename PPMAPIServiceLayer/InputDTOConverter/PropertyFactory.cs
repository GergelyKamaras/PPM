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
            if (ValidatePropertyData(property))
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

            throw new ArgumentException("Error creating property!");
        }

        public RentalProperty CreateRentalProperty(RentalPropertyInputDTO property)
        {
            if (ValidatePropertyData(property) && ValidateRentalPropertySpecificData(property))
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

            throw new ArgumentException("Error creating rental property!");
        }

        private Address CreateAddress(AddressDTO address)
        {
            if (ValidateInputDTOAddress(address))
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

            throw new ArgumentException("Error generating address!");
        }

        private bool ValidateInputDTOAddress(AddressDTO address)
        {
            if (address.City == null ||
                address.Country == null ||
                address.Street == null ||
                address.StreetNumber <= 0 ||
                address.ZipCode == null)
            {
                throw new ArgumentException("Invalid address data!");
            }

            return true;
        }

        private bool ValidatePropertyData(IPropertyInputDTO property)
        {
            if (property.Name == null ||
                property.OwnerId == null ||
                property.PurchaseDate == DateTime.MinValue ||
                property.Size <= 0 ||
                property.PurchasePrice <= 0)
            {
                throw new ArgumentException("Invalid basic property data!");
            }
            return true;
        }

        private bool ValidateRentalPropertySpecificData(RentalPropertyInputDTO property)
        {
            if (property.RentalFee <= 0)
            {
                throw new ArgumentException("Invalid rental property data!");
            }

            return true;
        }
    }
}
