using PPMModelLibrary.Models.FinancialObjects.ValueModifiers;

namespace PPMAPIDataAccess.DbTableQueries.ValueDecreasesQueries
{
    public class ValueDecreasesQueries : IValueDecreasesQueries
    {
        private readonly PPMDbContext _db;

        public ValueDecreasesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddValueDecrease(ValueDecrease valueDecrease)
        {
            _db.ValueDecreases.Add(valueDecrease);
            _db.SaveChanges();
        }

        public ValueDecrease GetValueDecreaseById(int id)
        {
            return _db.ValueDecreases.FirstOrDefault(r => r.Id == id);
        }

        public List<ValueDecrease> GetValueDecreaseByPropertyId(Guid id)
        {
            return _db.ValueDecreases.Where(r => r.Property.Id == id || r.RentalProperty.Id == id).ToList();
        }

        public void UpdateValueDecrease(ValueDecrease valueDecrease)
        {
            _db.ValueDecreases.Update(valueDecrease);
            _db.SaveChanges();
        }

        public void DeleteValueDecrease(int id)
        {
            _db.ValueDecreases.Remove(_db.ValueDecreases.FirstOrDefault(r => r.Id == id));
            _db.SaveChanges();
        }
    }
}
