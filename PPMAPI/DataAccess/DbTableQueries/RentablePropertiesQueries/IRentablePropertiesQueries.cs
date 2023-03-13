using PPMModelLibrary.Models.Properties;

namespace PPMAPI.DataAccess.DbTableQueries.RentalPropertiesQueries
{
    public interface IRentalPropertiesQueries
    {
        public void AddRentalProperty(RentalProperty rentalProperty);
        public void DeleteRentalProperty(Guid id);
        public void UpdateRentalProperty(RentalProperty rentalProperty);
        public RentalProperty GetRentalPropertyById(Guid id);
        public List<RentalProperty> GetRentalPropertiesByOwnerId(string id);
    }
}
