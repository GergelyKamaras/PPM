using PPMAPIModelLibrary.Properties;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMAPIModelLibrary.UtilityModels;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    internal class PropertyOutputDTOFactory : IPropertyOutputDTOFactory
    {
        public IPropertyOutputDTO CreatePropertyOutput(IProperty property)
        {
            bool propertyIsValid = ValidateProperty(property);
            bool addressIsValid = ValidateAddress(property.Address);

            if (propertyIsValid && addressIsValid && property is Property)
            {
                return MakePropertyOutputDTO(property);
            }
            else if (propertyIsValid && addressIsValid && property is RentalProperty && 
                     ValidateRentalProperty((RentalProperty)property))
            {
                return MakeRentalPropertyOutputDTO((RentalProperty)property);
            }
            else
            {
                throw new ArgumentException("Error! Invalid property object type!");
            }

        }

        public PropertyOutputDTO MakePropertyOutputDTO(IProperty property)
        {
            return new PropertyOutputDTO()
            {
                Id = property.Id.ToString(),
                Name = property.Name,
                Address = new AddressDTO()
                {
                    Country = property.Address.Country,
                    City = property.Address.City,
                    ZipCode = property.Address.ZipCode,
                    Street = property.Address.Street,
                    StreetNumber = property.Address.StreetNumber,
                    AdditionalInfo = property.Address.AdditionalInfo
                },
                Size = property.Size,
                OwnerId = property.Owner.UserId,
                TotalCost = 0,
                TotalRevenue = 0,
                CurrentValue = 0,
                Balance = 0
            };
        }

        public RentalPropertyOutputDTO MakeRentalPropertyOutputDTO(RentalProperty property)
        {
            return new RentalPropertyOutputDTO()
            {
                Id = property.Id.ToString(),
                Name = property.Name,
                Address = new AddressDTO()
                {
                    Country = property.Address.Country,
                    City = property.Address.City,
                    ZipCode = property.Address.ZipCode,
                    Street = property.Address.Street,
                    StreetNumber = property.Address.StreetNumber,
                    AdditionalInfo = property.Address.AdditionalInfo
                },
                Size = property.Size,
                OwnerId = property.Owner.UserId,
                TenantId = (property.Tenant == null) ? "" : property.Tenant.UserId,
                RentalFee = property.RentalFee,
                TotalCost = 0,
                TotalRevenue = 0,
                CurrentValue = 0,
                Balance = 0
            };
        }

        private bool ValidateAddress(Address address)
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
        
        private bool ValidateProperty(IProperty property)
        {
            if (property.Id == null ||
                property.Name == null ||
                property.Size <= 0 ||
                property.PurchasePrice <= 0 ||
                property.PurchaseDate == DateTime.MinValue ||
                property.Owner == null)
            {
                throw new ArgumentException("Error! Invalid property object properties!");
            }

            return true;
        }

        private bool ValidateRentalProperty(RentalProperty property)
        {
            if (property.RentalFee <= 0)
            {
                throw new ArgumentException("Error! Invalid rental property object properties!");
            }

            return true;
        }
    }
}
