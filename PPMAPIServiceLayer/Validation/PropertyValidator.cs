using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.UtilityModels;

namespace PPMAPIServiceLayer.Validation
{
    public interface IPropertyValidator
    {
        bool Validate(IProperty property);
    }

    public class PropertyValidator : IPropertyValidator
    {
        public bool Validate(IProperty property)
        {
            bool propertyIsValid = ValidateProperty(property);
            bool addressIsValid = ValidateAddress(property.Address);

            if (propertyIsValid && addressIsValid && property is Property)
            {
                return true;
            }
            else if (propertyIsValid && addressIsValid && property is RentalProperty &&
                     ValidateRentalProperty((RentalProperty)property))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ValidateAddress(Address address)
        {
            if (address.Country == null ||
                address.City == null ||
                address.ZipCode == null ||
                address.Street == null ||
                address.StreetNumber <= 0)
            {
                return false;
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
                return false;
            }

            return true;
        }

        private bool ValidateRentalProperty(RentalProperty property)
        {
            if (property.RentalFee <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
