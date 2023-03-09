using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPI.DataAccess.DbTableQueries.AddressQueries
{
    public class AddressesQueries : IAddressesQueries
    {
        private readonly PPMDbContext _db;

        public AddressesQueries(PPMDbContext dbContext)
        {
            _db = dbContext;
        }
        public void AddAddress(Address address)
        {
            _db.Addresses.Add(address);
            _db.SaveChanges();
        }

        public Address GetAddressById(int id)
        {
            return _db.Addresses.FirstOrDefault(a => a.Id == id);
        }

        public void UpdateAddress(Address address)
        {
            _db.Addresses.Update(address);
            _db.SaveChanges();
        }

        public void DeleteAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
