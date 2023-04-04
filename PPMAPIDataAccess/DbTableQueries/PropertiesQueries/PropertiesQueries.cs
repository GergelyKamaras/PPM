﻿using Microsoft.EntityFrameworkCore;
using PPMAPIModelLibrary.Properties;

namespace PPMAPIDataAccess.DbTableQueries.PropertiesQueries
{
    public class PropertiesQueries : IPropertiesQueries
    {
        private readonly PPMDbContext _db;

        public PropertiesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddProperty(Property property)
        {
            _db.Properties.Add(property);
            _db.SaveChanges();
        }

        public void DeleteProperty(string id)
        {
            var property = _db.Properties
                .Include(p => p.Address)
                .Include(p => p.Costs)
                .Include(p => p.Revenues)
                .Include(p => p.ValueIncreases)
                .Include(p => p.ValueIncreases)
                .FirstOrDefault(p => p.Id.ToString() == id);
            
            _db.Properties.Remove(property);
            _db.SaveChanges();
        }

        public void UpdateProperty(Property property)
        {
            _db.Properties.Update(property);
            _db.SaveChanges();
        }

        public Property GetPropertyById(string id)
        {
            return _db.Properties
                .Include(p => p.Address)
                .Include(p => p.Costs)
                .Include(p => p.Revenues)
                .Include(p => p.ValueIncreases)
                .Include(p => p.ValueIncreases)
                .FirstOrDefault(p => p.Id.ToString() == id);
        }

        public List<Property> GetPropertiesByOwnerId(string id)
        {
            return _db.Properties.Where(p => p.Owner.UserId == id)
                .Include(p => p.Address)
                .Include(p => p.Owner)
                .Include(p => p.Costs)
                .Include(p => p.Revenues)
                .Include(p => p.ValueDecreases)
                .Include(p => p.ValueIncreases)
                .ToList();
        }
    }
}
