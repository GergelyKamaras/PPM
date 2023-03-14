using PPMModelLibrary.Models.FinancialObjects.ValueModifiers;

namespace PPMAPIDataAccess.DbTableQueries.ValueDecreasesQueries
{
    public interface IValueDecreasesQueries
    {
        public void AddValueDecrease(ValueDecrease valueDecrease);
        public ValueDecrease GetValueDecreaseById(int id);
        public List<ValueDecrease> GetValueDecreaseByPropertyId(Guid id);
        public void UpdateValueDecrease(ValueDecrease valueDecrease);
        public void DeleteValueDecrease(int id);
    }
}
