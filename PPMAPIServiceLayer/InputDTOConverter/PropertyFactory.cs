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
                    Address = CreateAddress(property),
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
                    Address = CreateAddress(property),
                    Name = property.Name,
                    PurchaseDate = property.PurchaseDate,
                    PurchasePrice = property.PurchasePrice,
                    RentalFee = property.RentalFee,
                    Size = property.Size
                };
            }

            throw new ArgumentException("Error creating rental property!");
        }

        private Address CreateAddress(IPropertyInputDTO property)
        {
            if (ValidateInputDTOAddress(property))
            {
                return new Address()
                {
                    Country = property.Country,
                    City = property.City,
                    Street = property.Street,
                    StreetNumber = property.StreetNumber,
                    ZipCode = property.ZipCode,
                    AdditionalInfo = property.AdditionalInfo
                };
            }

            throw new ArgumentException("Error generating address!");
        }

        private bool ValidateInputDTOAddress(IPropertyInputDTO property)
        {
            if (property.City == null ||
                property.Country == null ||
                property.Street == null ||
                property.StreetNumber == null ||
                property.StreetNumber <= 0 ||
                property.ZipCode == null)
            {
                throw new ArgumentException("Invalid address data!");
            }

            return true;
        }

        private bool ValidatePropertyData(IPropertyInputDTO property)
        {
            if (property.Name == null ||
                property.OwnerId == null ||
                property.PurchaseDate == null ||
                property.PurchaseDate == DateTime.MinValue ||
                property.Size == null ||
                property.Size <= 0 ||
                property.PurchasePrice == null ||
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
                throw new ArgumentException("Invalid pental property data!");
            }

            return true;
        }
    }
}
