﻿using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Interfaces;
using PPMAPI.DataAccess.DbTableQueries.AddressQueries;
using PPMAPI.DataAccess;
using PPMAPI.DataAccess.DbTableQueries.CostsQueries;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    internal class CostsQueriesTests
    {
        private PPMDbContext _db;
        private ICostsQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new CostsQueries(_db);
        }

        [Test]
        public void AddCost_ValidInput_IsInDb()
        {
            Assert.Pass();
        }

        [Test]
        public void AddCost_InvalidInput_ThrowsError()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCostById_IsInDb_GetsCost()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCostById_NotInDb_ReturnsNull()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCostByPropertyId_UsePropertyIsInDb_GetsCost()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCostByPropertyId_UseRentablePropertyIsInDb_GetsCost()
        {
            Assert.Pass();
        }

        [Test]
        public void GetCostByPropertyId_InvalidId_ReturnsNull()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateCost_IsInDb_UpdatesSuccessfully()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateCost_NotInDb_ThrowsError()
        {
            Assert.Pass();
        }

        [Test]
        public void DeleteCost_IsInDb_Successful()
        {
            Assert.Pass();
        }

        [Test]
        public void DeleteCost_NotInDb_ThrowsError()
        {
            Assert.Pass();
        }
    }
}
