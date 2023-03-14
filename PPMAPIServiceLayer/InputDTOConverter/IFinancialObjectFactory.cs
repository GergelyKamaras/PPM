using PPMDTOModelLibrary.InputDTOs.FinancialInput;
using PPMModelLibrary.Models.FinancialObjects;
using PPMModelLibrary.Models.FinancialObjects.Transactions;
using PPMModelLibrary.Models.FinancialObjects.ValueModifiers;

namespace PPMAPIServiceLayer.InputDTOConverter;

public interface IFinancialObjectFactory
{
    IFinancialObject CreateFinancialObject(FinancialInputDTO inputDTO);
    
}