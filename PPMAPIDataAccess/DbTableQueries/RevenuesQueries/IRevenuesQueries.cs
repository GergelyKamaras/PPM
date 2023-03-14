using PPMModelLibrary.Models.Transactions;

namespace PPMAPIDataAccess.DbTableQueries.RevenuesQueries
{
    public interface IRevenuesQueries
    {
        public void AddRevenue(Revenue revenue);
        public Revenue GetRevenueById(int id);
        public List<Revenue> GetRevenueByPropertyId(Guid id);
        public void UpdateRevenue(Revenue revenue);
        public void DeleteRevenue(int id);
     
    }
}
