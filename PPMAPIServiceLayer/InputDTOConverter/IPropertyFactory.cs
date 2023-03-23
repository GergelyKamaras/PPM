using PPMAPIModelLibrary.Properties;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public interface IPropertyFactory
    {
        public Property CreateProperty(PropertyInputDTO property);
        public RentalProperty CreateRentalProperty(PropertyInputDTO property);
    }
}
