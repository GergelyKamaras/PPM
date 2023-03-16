using PPMAPIModelLibrary.Users;

namespace PPMAPIDataAccess.DbTableQueries.OwnersQueries
{
    public interface IOwnersQueries
    {
        public void AddOwner(Owner owner);
        public void DeleteOwner(string id);
        public void UpdateOwner(Owner owner);
        public Owner GetOwnerById(string id);
        public Owner GetOwnerByPropertyId(Guid id);
    }
}
