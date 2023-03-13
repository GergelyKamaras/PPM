using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPI.DataAccess.DbTableQueries.ValueDecreasesQueries
{
    public class ValueDecreasesQueries : IValueDecreasesQueries
    {
        private readonly PPMDbContext _db;

        public ValueDecreasesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddValueDecrease(ValueDecrease ValueDecrease)
        {
            _db.ValueDecreases.Add(ValueDecrease);
            _db.SaveChanges();
        }

        public ValueDecrease GetValueDecreaseById(int id)
        {
            return _db.ValueDecreases.FirstOrDefault(r => r.Id == id);
        }

        public List<ValueDecrease> GetValueDecreaseByPropertyId(Guid id)
        {
            return _db.ValueDecreases.Where(r => r.Property.Id == id || r.RentableProperty.Id == id).ToList();
        }

        public void UpdateValueDecrease(ValueDecrease ValueDecrease)
        {
            _db.ValueDecreases.Update(ValueDecrease);
            _db.SaveChanges();
        }

        public void DeleteValueDecrease(int id)
        {
            _db.ValueDecreases.Remove(_db.ValueDecreases.FirstOrDefault(r => r.Id == id));
            _db.SaveChanges();
        }
    }
}
