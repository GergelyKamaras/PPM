using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPI.DataAccess.DbTableQueries.ValueIncreasesQueries
{
    public class ValueIncreasesQueries : IValueIncreasesQueries
    {
        private readonly PPMDbContext _db;

        public ValueIncreasesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddValueIncrease(ValueIncrease ValueIncrease)
        {
            _db.ValueIncreases.Add(ValueIncrease);
            _db.SaveChanges();
        }

        public ValueIncrease GetValueIncreaseById(int id)
        {
            return _db.ValueIncreases.FirstOrDefault(r => r.Id == id);
        }

        public List<ValueIncrease> GetValueIncreaseByPropertyId(Guid id)
        {
            return _db.ValueIncreases.Where(r => r.Property.Id == id || r.RentableProperty.Id == id).ToList();
        }

        public void UpdateValueIncrease(ValueIncrease ValueIncrease)
        {
            _db.ValueIncreases.Update(ValueIncrease);
            _db.SaveChanges();
        }

        public void DeleteValueIncrease(int id)
        {
            _db.ValueIncreases.Remove(_db.ValueIncreases.FirstOrDefault(r => r.Id == id));
            _db.SaveChanges();
        }
    }
}
