using PPMModelLibrary.Models.Users;

namespace PPMAPIDataAccess.DbTableQueries.OwnersQueries
{
    public class OwnersQueries : IOwnersQueries
    {
        private readonly PPMDbContext _db;
        public OwnersQueries(PPMDbContext db)
        {
            _db = db;
        }

        public void AddOwner(Owner owner)
        {
            _db.Owners.Add(owner);
            _db.SaveChanges();
        }

        public void DeleteOwner(string id)
        {
            _db.Owners.Remove(_db.Owners.FirstOrDefault(o => o.UserId == id));
            _db.SaveChanges();
        }

        public void UpdateOwner(Owner owner)
        {
            _db.Owners.Update(owner);
            _db.SaveChanges();
        }

        public Owner GetOwnerById(string id)
        {
            return _db.Owners.FirstOrDefault(o => o.UserId == id);
        }

        public Owner GetOwnerByPropertyId(Guid id)
        {
            return _db.Owners.FirstOrDefault(o => o.Properties.Any(p => p.Id == id) || o.RentalProperties.Any(p => p.Id == id));
        }
    }
}
