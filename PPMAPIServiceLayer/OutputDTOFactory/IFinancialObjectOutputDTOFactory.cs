using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    internal interface IFinancialObjectOutputDTOFactory
    {
        public IFinancialObjectOutputDTO Create(IFinancialObject finObject);
    }
}
