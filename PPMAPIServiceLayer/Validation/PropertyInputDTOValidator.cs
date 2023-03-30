using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayer.Validation
{
    public interface IPropertyInputDTOValidator
    {
        bool Validate(PropertyInputDTO protoProperty);
    }

    public class PropertyInputDTOValidator : IPropertyInputDTOValidator
    {

        public bool Validate(PropertyInputDTO protoProperty)
        {
            if (!ValidatePropertyData(protoProperty) ||
                !ValidateInputDTOAddress(protoProperty.Address) ||
                (protoProperty.IsRental && !ValidateRentalPropertySpecificData(protoProperty)))
            {
                return false;
            }
            return true;
        }

        private bool ValidateInputDTOAddress(AddressDTO address)
        {
            if (address.City == null ||
                address.Country == null ||
                address.Street == null ||
                address.StreetNumber <= 0 ||
                address.ZipCode == null)
            {
                return false;
            }

            return true;
        }

        private bool ValidatePropertyData(PropertyInputDTO property)
        {
            if (property.Name == null ||
                property.OwnerId == null ||
                property.PurchaseDate == DateTime.MinValue ||
                property.Size <= 0 ||
                property.PurchasePrice <= 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateRentalPropertySpecificData(PropertyInputDTO property)
        {
            if (property.RentalFee < 0)
            {
                return false;
            }

            return true;
        }
    }
}
