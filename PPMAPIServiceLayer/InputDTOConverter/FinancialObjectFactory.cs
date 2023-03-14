using PPMDTOModelLibrary.InputDTOs.FinancialInput;
using PPMModelLibrary.Models.FinancialObjects;
using PPMModelLibrary.Models.FinancialObjects.Transactions;
using PPMModelLibrary.Models.FinancialObjects.ValueModifiers;

namespace PPMAPIServiceLayer.InputDTOConverter
{
    public class FinancialObjectFactory : IFinancialObjectFactory
    {
        private readonly string[] _financialObjectTypes = 
        {
            "Cost",
            "Revenue",
            "ValueIncrease",
            "ValueDecrease"
        };

        public IFinancialObject CreateFinancialObject(FinancialInputDTO inputDTO)
        {
            if (inputDTO.Value <= 0 ||
                inputDTO.PropertyId == null ||
                inputDTO.Title == null ||
                inputDTO.Date == DateTime.MinValue ||
                _financialObjectTypes.All(t => t != inputDTO.FinancialObjectType))
            {
                throw new ArgumentException();
            }

            switch (inputDTO.FinancialObjectType)
            {
                case "Cost":
                    return CreateCost(inputDTO);
                case "Revenue":
                    return CreateRevenue(inputDTO);
                case "ValueIncrease":
                    return CreateValueIncrease(inputDTO);
                case "ValueDecrease":
                    return CreateValueDecrease(inputDTO);
                default:
                    throw new ArgumentException("Invalid financial object type!");

            }
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
