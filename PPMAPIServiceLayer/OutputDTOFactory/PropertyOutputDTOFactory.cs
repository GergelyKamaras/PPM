using PPMAPIModelLibrary.Properties;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIDTOModelLibrary.SharedDTOs;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    public class PropertyOutputDTOFactory : IPropertyOutputDTOFactory
    {
        public IPropertyOutputDTO CreatePropertyOutputDTO(IProperty property)
        {
            if (property is Property)
            {
                return MakePropertyOutputDTO(property);
            }
            else 
            {
                return MakeRentalPropertyOutputDTO((RentalProperty)property);
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
    }
}
