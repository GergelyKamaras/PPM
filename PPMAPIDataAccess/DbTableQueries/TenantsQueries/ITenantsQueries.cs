﻿using PPMAPIModelLibrary.Users;

namespace PPMAPIDataAccess.DbTableQueries.TenantsQueries
{
    public interface ITenantsQueries
    {
        public void AddTenant(Tenant tenant);
        public void DeleteTenant(string id);
        public void UpdateTenant(Tenant tenant);
        public Tenant GetTenantById(string id);
        public Tenant GetTenantByPropertyId(Guid id);
    }
}
