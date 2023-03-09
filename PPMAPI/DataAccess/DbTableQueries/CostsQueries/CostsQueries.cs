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

        public List<Cost> GetCostByPropertyId(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCost(Cost cost)
        {
            throw new NotImplementedException();
        }

        public void DeleteCost(int id)
        {
            throw new NotImplementedException();
        }
    }
}
