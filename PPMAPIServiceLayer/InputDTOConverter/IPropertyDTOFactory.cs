using PPMDTOModelLibrary.InputDTOs.Properties;
using PPMModelLibrary.Models.Properties;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public interface IPropertyDTOFactory
    {
        public Property CreateProperty(PropertyInputDTO property);
        public RentalProperty CreateRentalProperty(RentalPropertyInputDTO property);
    }
}
