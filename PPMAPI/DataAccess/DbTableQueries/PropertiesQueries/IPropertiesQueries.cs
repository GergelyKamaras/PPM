using PPMModelLibrary.Models.Properties;

namespace PPMAPI.DataAccess.DbTableQueries.PropertiesQueries
{
    public interface IPropertiesQueries
    {
        public void AddProperty(Property property);
        public void DeleteProperty(Guid id);
        public void UpdateProperty(Property property);
        public Property GetPropertyById(Guid id);
        public List<Property> GetPropertiesByOwnerId(string id);
    }
}
