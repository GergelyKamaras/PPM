using PPMAPIModelLibrary.Properties;

namespace PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries
{
    public interface IRentalPropertiesQueries
    {
        public void AddRentalProperty(RentalProperty rentalProperty);
        public void DeleteRentalProperty(string id);
        public void UpdateRentalProperty(RentalProperty rentalProperty);
        public RentalProperty GetRentalPropertyById(string id);
        public List<RentalProperty> GetRentalPropertiesByOwnerId(string id);
    }
}
