using PPMAPIModelLibrary.Properties;

namespace PPMAPIDataAccess.DbTableQueries.PropertiesQueries
{
    public class PropertiesQueries : IPropertiesQueries
    {
        private readonly PPMDbContext _db;

        public PropertiesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddProperty(Property property)
        {
            _db.Properties.Add(property);
            _db.SaveChanges();
        }

        public void DeleteProperty(Guid id)
        {
            _db.Properties.Remove(_db.Properties.FirstOrDefault(p => p.Id == id));
            _db.SaveChanges();
        }

        public void UpdateProperty(Property property)
        {
            _db.Properties.Update(property);
            _db.SaveChanges();
        }

        public Property GetPropertyById(Guid id)
        {
            return _db.Properties.FirstOrDefault(p => p.Id == id);
        }

        public List<Property> GetPropertiesByOwnerId(string id)
        {
            return _db.Properties.Where(p => p.Owner.UserId == id).ToList();
        }
    }
}
