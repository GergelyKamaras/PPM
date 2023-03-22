using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayer.Validation
{
    public interface IPropertyInputDTOValidator
    {
        bool Validate(IPropertyInputDTO protoProperty);
    }

    public class PropertyInputDTOValidator : IPropertyInputDTOValidator
    {

        public bool Validate(IPropertyInputDTO protoProperty)
        {
            if (!ValidatePropertyData(protoProperty) ||
                !ValidateInputDTOAddress(protoProperty.Address) ||
                (protoProperty.IsRental && !ValidateRentalPropertySpecificData((RentalPropertyInputDTO)protoProperty)))
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

        private bool ValidatePropertyData(IPropertyInputDTO property)
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

        private bool ValidateRentalPropertySpecificData(RentalPropertyInputDTO property)
        {
            if (property.RentalFee <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
