using PPMAPIModelLibrary.Properties;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public interface IPropertyInputDTOFactory
    {
        public Property CreateProperty(PropertyInputDTO property);
        public RentalProperty CreateRentalProperty(RentalPropertyInputDTO property);
    }
}
