using PPMAPIModelLibrary.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public class FinancialObjectFactory : IFinancialObjectFactory
    {
        public IFinancialObject CreateFinancialObject(FinancialInputDTO inputDTO)
        {
            if (!ValidateFinancialInputDTO(inputDTO))
            {
                throw new ArgumentException();
            }

            switch (inputDTO.FinancialObjectType)
            {
                case FinancialObject.Cost:
                    return CreateCost(inputDTO);
                case FinancialObject.Revenue:
                    return CreateRevenue(inputDTO);
                case FinancialObject.ValueIncrease:
                    return CreateValueIncrease(inputDTO);
                case FinancialObject.ValueDecrease:
                    return CreateValueDecrease(inputDTO);
                default:
                    throw new ArgumentException("Invalid financial object type!");

            }
        }

        private bool ValidateFinancialInputDTO(FinancialInputDTO inputDTO)
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

        private Cost CreateCost(FinancialInputDTO inputDTO)
        {
            return new Cost()
            {
                Title = inputDTO.Title,
                Description = inputDTO.Description,
                Value = inputDTO.Value,
                Date = inputDTO.Date,
            };
        }

        private Revenue CreateRevenue(FinancialInputDTO inputDTO)
        {
            return new Revenue()
            {
                Title = inputDTO.Title,
                Description = inputDTO.Description,
                Value = inputDTO.Value,
                Date = inputDTO.Date,
            };
        }

        private ValueIncrease CreateValueIncrease(FinancialInputDTO inputDTO)
        {
            return new ValueIncrease()
            {
                Title = inputDTO.Title,
                Description = inputDTO.Description,
                Value = inputDTO.Value,
                Date = inputDTO.Date,
            };
        }

        private ValueDecrease CreateValueDecrease(FinancialInputDTO inputDTO)
        {
            return new ValueDecrease()
            {
                Title = inputDTO.Title,
                Description = inputDTO.Description,
                Value = inputDTO.Value,
                Date = inputDTO.Date,
            };
        }
    }
}
