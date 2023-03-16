using PPMAPIModelLibrary.Properties;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    public interface IPropertyOutputDTOFactory
    {
        public IPropertyOutputDTO CreatePropertyOutputDTO(IProperty property);
    }
}
