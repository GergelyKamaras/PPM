using PPMAPIModelLibrary.FinancialObjects;

namespace PPMAPIServiceLayer.Validation
{
    public interface IFinancialObjectValidator
    {
        bool Validate(IFinancialObject finObject);
    }

    public class FinancialObjectValidator : IFinancialObjectValidator
    {
        public bool Validate(IFinancialObject finObject)
        {
            if ((finObject.Property == null && finObject.RentalProperty == null) || 
                finObject.Id == 0 ||
                finObject.Title == null ||
                finObject.Date == DateTime.MinValue ||
                finObject.Value <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
