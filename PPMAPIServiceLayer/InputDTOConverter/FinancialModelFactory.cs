using PPMDTOModelLibrary.InputDTOs.FinancialInput;
using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public class FinancialModelFactory : IFinancialModelFactory
    {
        public Cost CreateCost(FinancialInputDTO inputDTO)
        {
            if (inputDTO.Value <= 0 ||
                inputDTO.PropertyId == null ||
                inputDTO.Title == null ||
                inputDTO.Date == DateTime.MinValue)
            {
                throw new ArgumentException();
            }

            return new Cost()
            {
                Title = inputDTO.Title,
                Description = inputDTO.Description,
                Value = inputDTO.Value,
                Date = inputDTO.Date,
            };
        }

        public Revenue CreateRevenue (FinancialInputDTO inputDTO)
        {
            throw new NotImplementedException();
        }

        public ValueIncrease CreateValueIncrease(FinancialInputDTO inputDTO)
        {
            throw new NotImplementedException();
        }

        public ValueDecrease CreateValueDecrease(FinancialInputDTO inputDTO)
        {
            throw new NotImplementedException();
        }
    }
}
