using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    public interface IFinancialObjectOutputDTOFactory
    {
        public IFinancialObjectOutputDTO Create(IFinancialObject finObject);
    }
}
