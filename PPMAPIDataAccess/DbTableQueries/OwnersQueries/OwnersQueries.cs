using Microsoft.EntityFrameworkCore;
using PPMAPIModelLibrary.Users;

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
            var owner = _db.Owners
                .Include(o => o.Properties)
                .Include(o => o.RentalProperties)
                .FirstOrDefault(o => o.UserId == id);

            _db.Owners.Remove(owner);
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
            var owner = _db.Owners
                .Include(o => o.Properties)
                .Include(o => o.RentalProperties)
                .FirstOrDefault(o => o.Properties.Any(p => p.Id == id) || o.RentalProperties.Any(p => p.Id == id));
            
            return owner;
        }
    }
}
