using PPMModelLibrary.Models.Transactions;

namespace PPMAPI.DataAccess.DbTableQueries.CostsQueries
{
    public class CostsQueries : ICostsQueries
    {
        private readonly PPMDbContext _db;

        public CostsQueries(PPMDbContext db)
        {
            _db = db;
        }

        public void AddCost(Cost cost)
        {
            _db.Costs.Add(cost);
            _db.SaveChanges();
        }

        public Cost GetCostById(int id)
        {
            return _db.Costs.FirstOrDefault(c => c.Id == id);
        }

        public List<Cost> GetCostByPropertyId(Guid id)
        {
            return _db.Costs.Where(c => c.Property.Id == id || c.RentableProperty.Id == id).ToList();
        }

        public void UpdateCost(Cost cost)
        {
            _db.Costs.Update(cost);
            _db.SaveChanges();
        }

        public void DeleteCost(int id)
        {
            _db.Costs.Remove(_db.Costs.FirstOrDefault(c => c.Id == id));
        }
    }
}
