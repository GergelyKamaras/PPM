using PPMModelLibrary.Models.Properties;

namespace PPMAPI.DataAccess.DbTableQueries.RentablePropertiesQueries
{
    public class RentablePropertiesQueries : IRentablePropertiesQueries
    {
        private readonly PPMDbContext _db;

        public RentablePropertiesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddRentableProperty(RentableProperty RentableProperty)
        {
            _db.RentableProperties.Add(RentableProperty);
            _db.SaveChanges();
        }

        public void DeleteRentableProperty(Guid id)
        {
            _db.RentableProperties.Remove(_db.RentableProperties.FirstOrDefault(p => p.Id == id));
            _db.SaveChanges();
        }

        public void UpdateRentableProperty(RentableProperty RentableProperty)
        {
            _db.RentableProperties.Update(RentableProperty);
            _db.SaveChanges();
        }

        public RentableProperty GetRentablePropertyById(Guid id)
        {
            return _db.RentableProperties.FirstOrDefault(p => p.Id == id);
        }

        public List<RentableProperty> GetRentablePropertiesByOwnerId(string id)
        {
            return _db.RentableProperties.Where(p => p.Owner.UserId == id).ToList();
        }
    }
}
