using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPI.DataAccess.DbTableQueries.ValueIncreasesQueries
{
    public interface IValueIncreasesQueries
    {
        public void AddValueIncrease(ValueIncrease ValueIncrease);
        public ValueIncrease GetValueIncreaseById(int id);
        public List<ValueIncrease> GetValueIncreaseByPropertyId(Guid id);
        public void UpdateValueIncrease(ValueIncrease ValueIncrease);
        public void DeleteValueIncrease(int id);
    }
}
