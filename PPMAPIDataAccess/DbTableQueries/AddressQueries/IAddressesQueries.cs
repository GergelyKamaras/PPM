using PPMAPIModelLibrary.UtilityModels;

namespace PPMAPIDataAccess.DbTableQueries.AddressQueries
{
    public interface IAddressesQueries
    {
        public void AddAddress(Address address);
        public Address GetAddressById(int id);
        public void UpdateAddress(Address address);
        public void DeleteAddress(int id);
    }
}
