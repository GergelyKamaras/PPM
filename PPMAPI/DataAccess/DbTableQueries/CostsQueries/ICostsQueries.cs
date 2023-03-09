using PPMModelLibrary.Models.Transactions;

namespace PPMAPI.DataAccess.DbTableQueries.CostsQueries
{
    public interface ICostsQueries
    {
        public void AddCost(Cost cost);
        public Cost GetCostById(int id);
        public List<Cost> GetCostByPropertyId(string id);
        public void UpdateCost(Cost cost);
        public void DeleteCost(int id);
    }
}
