using PPMModelLibrary.Models.Transactions;

namespace PPMAPI.DataAccess.DbTableQueries.RevenuesQueries
{
    public class RevenuesQueries : IRevenuesQueries
    {
        private readonly PPMDbContext _db;

        public RevenuesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddRevenue(Revenue revenue)
        {
            _db.Revenues.Add(revenue);
            _db.SaveChanges();
        }

        public Revenue GetRevenueById(int id)
        {
            return _db.Revenues.FirstOrDefault(r => r.Id == id);
        }

        public List<Revenue> GetRevenueByPropertyId(Guid id)
        {
            return _db.Revenues.Where(r => r.Property.Id == id || r.RentalProperty.Id == id).ToList();
        }

        public void UpdateRevenue(Revenue revenue)
        {
            _db.Revenues.Update(revenue);
            _db.SaveChanges();
        }

        public void DeleteRevenue(int id)
        {
            _db.Revenues.Remove(_db.Revenues.FirstOrDefault(r => r.Id == id));
            _db.SaveChanges();
        }
    }
}
