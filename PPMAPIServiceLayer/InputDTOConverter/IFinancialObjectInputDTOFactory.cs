using PPMAPIModelLibrary.FinancialObjects;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPIServiceLayer.InputDTOConverter;

public interface IFinancialObjectInputDTOFactory
{
    IFinancialObject CreateFinancialObject(FinancialInputDTO inputDTO);
    
}