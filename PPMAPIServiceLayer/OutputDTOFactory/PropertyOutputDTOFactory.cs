using PPMAPIModelLibrary.Properties;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMAPIModelLibrary.UtilityModels;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    public class PropertyOutputDTOFactory : IPropertyOutputDTOFactory
    {
        public IPropertyOutputDTO CreatePropertyOutputDTO(IProperty property)
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
                PurchasePrice = property.PurchasePrice,
                PurchaseDate = property.PurchaseDate,
                OwnerId = property.Owner.UserId,
                TotalCost = property.Costs.Sum(c => c.Value),
                TotalRevenue = property.Revenues.Sum(r => r.Value),
                CurrentValue = property.ValueIncreases.Sum(v => v.Value) - 
                               property.ValueDecreases.Sum(v => v.Value),
                Balance = property.Revenues.Sum(r => r.Value) - property.Costs.Sum(c => c.Value)
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
                PurchasePrice = property.PurchasePrice,
                PurchaseDate = property.PurchaseDate,
                TenantId = (property.Tenant == null) ? "" : property.Tenant.UserId,
                RentalFee = property.RentalFee,
                TotalCost = property.Costs.Sum(c => c.Value),
                TotalRevenue = property.Revenues.Sum(r => r.Value),
                CurrentValue = property.ValueIncreases.Sum(v => v.Value) -
                               property.ValueDecreases.Sum(v => v.Value),
                Balance = property.Revenues.Sum(r => r.Value) - property.Costs.Sum(c => c.Value)
            };
        }

        private bool ValidateAddress(Address address)
        {
            if (address.Country == null ||
                address.City == null ||
                address.ZipCode == null||
                address.Street == null ||
                address.StreetNumber <= 0)
            {
                throw new ArgumentException("Invalid address data!");
            }

            return true;
        }
        
        private bool ValidateProperty(IProperty property)
        {
            if (property.Id == null ||
                property.Id == Guid.Empty ||
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
