using PPMModelLibrary.Models.FinancialObjects.Transactions;

namespace PPMAPIDataAccess.DbTableQueries.CostsQueries
{
    public interface ICostsQueries
    {
        public void AddCost(Cost cost);
        public Cost GetCostById(int id);
        public List<Cost> GetCostByPropertyId(Guid id);
        public void UpdateCost(Cost cost);
        public void DeleteCost(int id);
    }
}
