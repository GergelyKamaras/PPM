using PPMAPIModelLibrary.Properties;

namespace PPMAPIDataAccess.DbTableQueries.PropertiesQueries
{
    public interface IPropertiesQueries
    {
        public void AddProperty(Property property);
        public void DeleteProperty(string id);
        public void UpdateProperty(Property property);
        public Property GetPropertyById(string id);
        public List<Property> GetPropertiesByOwnerId(string id);
    }
}
