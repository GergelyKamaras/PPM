using PPMAPIModelLibrary.FinancialObjects;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPIServiceLayer.InputDTOConverter;

public interface IFinancialObjectFactory
{
    IFinancialObject CreateFinancialObject(FinancialInputDTO inputDTO);
    
}