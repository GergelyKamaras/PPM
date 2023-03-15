using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;

namespace PPMAPIServiceLayer.OutputDTOConverter
{
    public class FinancialObjectOutputDTOFactory : IFinancialObjectOutputDTOFactory
    {
        public IFinancialObjectOutputDTO Convert(IFinancialObject finObject)
        {
            ValidateInput(finObject);
            
            IFinancialObjectOutputDTO output = CreateFinancialObjectOutputDTO(finObject);

            if (finObject is Cost)
            {
                output.FinancialObjectType = "Cost";
            }

            if (finObject is Revenue)
            {
                output.FinancialObjectType = "Revenue";
            }

            if (finObject is ValueIncrease)
            {
                output.FinancialObjectType = "ValueIncrease";
            }

            if (finObject is ValueDecrease)
            {
                output.FinancialObjectType = "ValueDecrease";
            }

            return output;
        }

        private void ValidateInput(IFinancialObject finObject)
        {
            if (finObject.Property == null && finObject.RentalProperty == null)
            {
                throw new ArgumentException("Error, no connected properties!");
            }

            if (finObject.Id == 0 ||
                finObject.Title == null ||
                finObject.Date == DateTime.MinValue ||
                finObject.Value <= 0)
            {
                throw new ArgumentException("Error, finan cial object has missing or invalid properties!");
            }
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
