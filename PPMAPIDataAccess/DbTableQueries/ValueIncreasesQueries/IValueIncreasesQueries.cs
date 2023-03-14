using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPIDataAccess.DbTableQueries.ValueIncreasesQueries
{
    public interface IValueIncreasesQueries
    {
        public void AddValueIncrease(ValueIncrease valueIncrease);
        public ValueIncrease GetValueIncreaseById(int id);
        public List<ValueIncrease> GetValueIncreaseByPropertyId(Guid id);
        public void UpdateValueIncrease(ValueIncrease valueIncrease);
        public void DeleteValueIncrease(int id);
    }
}
