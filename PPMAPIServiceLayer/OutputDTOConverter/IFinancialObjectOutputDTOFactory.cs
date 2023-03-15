using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;

namespace PPMAPIServiceLayer.OutputDTOConverter
{
    internal interface IFinancialObjectOutputDTOFactory
    {
        public IFinancialObjectOutputDTO Convert(IFinancialObject finObject);
    }
}
