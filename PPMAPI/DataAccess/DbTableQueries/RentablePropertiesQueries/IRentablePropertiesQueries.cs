using PPMModelLibrary.Models.Properties;

namespace PPMAPI.DataAccess.DbTableQueries.RentablePropertiesQueries
{
    public interface IRentablePropertiesQueries
    {
        public void AddRentableProperty(RentableProperty RentableProperty);
        public void DeleteRentableProperty(Guid id);
        public void UpdateRentableProperty(RentableProperty RentableProperty);
        public RentableProperty GetRentablePropertyById(Guid id);
        public List<RentableProperty> GetRentablePropertiesByOwnerId(string id);
    }
}
