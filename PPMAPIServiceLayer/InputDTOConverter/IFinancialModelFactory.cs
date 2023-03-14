using PPMDTOModelLibrary.InputDTOs.FinancialInput;
using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPIServiceLayer.InputDTOConverter;

public interface IFinancialModelFactory
{
    Cost CreateCost(FinancialInputDTO inputDTO);
    Revenue CreateRevenue (FinancialInputDTO inputDTO);
    ValueIncrease CreateValueIncrease(FinancialInputDTO inputDTO);
    ValueDecrease CreateValueDecrease(FinancialInputDTO inputDTO);
}