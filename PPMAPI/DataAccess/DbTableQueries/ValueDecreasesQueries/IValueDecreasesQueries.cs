using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPI.DataAccess.DbTableQueries.ValueDecreasesQueries
{
    public interface IValueDecreasesQueries
    {
        public void AddValueDecrease(ValueDecrease ValueDecrease);
        public ValueDecrease GetValueDecreaseById(int id);
        public List<ValueDecrease> GetValueDecreaseByPropertyId(Guid id);
        public void UpdateValueDecrease(ValueDecrease ValueDecrease);
        public void DeleteValueDecrease(int id);
    }
}
