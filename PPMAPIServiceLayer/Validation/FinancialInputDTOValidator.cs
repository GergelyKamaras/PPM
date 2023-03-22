using PPMAPIModelLibrary.FinancialObjects;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPIServiceLayer.Validation
{
    public interface IFinancialInputDTOValidator
    {
        bool Validate(FinancialInputDTO inputDTO);
    }

    public class FinancialInputDTOValidator : IFinancialInputDTOValidator
    {
        public bool Validate(FinancialInputDTO inputDTO)
        {
            if (inputDTO.Value <= 0 ||
                inputDTO.PropertyId == null ||
                inputDTO.Title == null ||
                inputDTO.Date == DateTime.MinValue ||
                FinancialObject.Types.All(t => t != inputDTO.FinancialObjectType))
            {
                return false;
            }

            return true;
        }
    }
}
