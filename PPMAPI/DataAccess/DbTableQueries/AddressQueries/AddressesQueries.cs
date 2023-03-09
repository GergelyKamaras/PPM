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
            throw new NotImplementedException();
        }

        public void UpdateAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public void DeleteAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
