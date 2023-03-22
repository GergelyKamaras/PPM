using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;

namespace PPMAPIServiceLayer.OutputDTOFactory
{
    public class FinancialObjectOutputDTOFactory : IFinancialObjectOutputDTOFactory
    {
        public IFinancialObjectOutputDTO Create(IFinancialObject finObject)
        {
            IFinancialObjectOutputDTO output = CreateFinancialObjectOutputDTO(finObject);

            if (finObject is Cost)
            {
                output.FinancialObjectType = FinancialObject.Cost;
            }

            if (finObject is Revenue)
            {
                output.FinancialObjectType = FinancialObject.Revenue;
            }

            if (finObject is ValueIncrease)
            {
                output.FinancialObjectType = FinancialObject.ValueIncrease;
            }

            if (finObject is ValueDecrease)
            {
                output.FinancialObjectType = FinancialObject.ValueDecrease;
            }

            return output;
        }

        private FinancialObjectOutputDTO CreateFinancialObjectOutputDTO(IFinancialObject finObject)
        {
            return new FinancialObjectOutputDTO()
            {
                Id = finObject.Id,
                Title = finObject.Title,
                Value = finObject.Value,
                Date = finObject.Date,
                Description = finObject.Description,
                IsRental = (finObject.Property == null),
                PropertyId = (finObject.Property == null) ? finObject.RentalProperty.Id.ToString() : finObject.Property.Id.ToString(),
            };
        }
    }
}
