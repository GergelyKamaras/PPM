﻿using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.Users;
using PPMAPIModelLibrary.UtilityModels;
using PPMAPIServiceLayer.OutputDTOFactory;
using System.Drawing;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;

namespace PPMAPIServiceLayerTests
{
    internal class PropertyOutputDTOFactoryTests
    {
        private PropertyOutputDTOFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new PropertyOutputDTOFactory();
        }

        [Test]
        public void CreatePropertyOutputDTO_ValidProperty_DataMatches()
        {
            Owner owner = new Owner()
            {
                UserId = Guid.NewGuid().ToString()
            };

            Property property = new Property()
            {
                Id = Guid.NewGuid(),
                Name = "ARealProperty",
                Owner = owner,
                Address = new Address()
                {
                    Country = "Corruptistan",
                    City = "Cesspit",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                },
                PurchaseDate = DateTime.Now,
                PurchasePrice = 50,
                Size = 35
            };

            IPropertyOutputDTO outProperty = _factory.CreatePropertyOutputDTO(property);

            Assert.Multiple(() =>
            {
                Assert.That(outProperty, Is.TypeOf(typeof(PropertyOutputDTO)));
                Assert.That(outProperty.Name, Is.EqualTo(property.Name));
                Assert.That(outProperty.Id, Is.EqualTo(property.Id.ToString()));
                Assert.That(outProperty.PurchaseDate, Is.EqualTo(property.PurchaseDate));
                Assert.That(outProperty.PurchasePrice, Is.EqualTo(property.PurchasePrice));
                Assert.That(outProperty.Size, Is.EqualTo(property.Size));
                Assert.That(outProperty.OwnerId, Is.EqualTo(property.Owner.UserId));
                Assert.That(outProperty.Address.Country, Is.EqualTo(property.Address.Country));
                Assert.That(outProperty.Address.City, Is.EqualTo(property.Address.City));
                Assert.That(outProperty.Address.ZipCode, Is.EqualTo(property.Address.ZipCode));
                Assert.That(outProperty.Address.Street, Is.EqualTo(property.Address.Street));
                Assert.That(outProperty.Address.StreetNumber, Is.EqualTo(property.Address.StreetNumber));
            });
        }

        [Test]
        public void CreatePropertyOutputDTO_ValidRentalProperty_DataMatches()
        {
            Owner owner = new Owner()
            {
                UserId = Guid.NewGuid().ToString()
            };

            Tenant tenant = new Tenant()
            {
                UserId = Guid.NewGuid().ToString()
            };

            RentalProperty property = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Name = "ARealProperty",
                Owner = owner,
                Address = new Address()
                {
                    Country = "Corruptistan",
                    City = "Cesspit",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                },
                PurchaseDate = DateTime.Now,
                PurchasePrice = 50,
                Size = 35,
                Tenant = tenant,
                RentalFee = 60
            };

            IPropertyOutputDTO protoProperty = _factory.CreatePropertyOutputDTO(property);
            RentalPropertyOutputDTO outProperty = (RentalPropertyOutputDTO)protoProperty;

            Assert.Multiple(() =>
            {
                Assert.That(outProperty, Is.TypeOf(typeof(RentalPropertyOutputDTO)));
                Assert.That(outProperty.Name, Is.EqualTo(property.Name));
                Assert.That(outProperty.Id, Is.EqualTo(property.Id.ToString()));
                Assert.That(outProperty.PurchaseDate, Is.EqualTo(property.PurchaseDate));
                Assert.That(outProperty.PurchasePrice, Is.EqualTo(property.PurchasePrice));
                Assert.That(outProperty.Size, Is.EqualTo(property.Size));
                Assert.That(outProperty.OwnerId, Is.EqualTo(property.Owner.UserId));
                Assert.That(outProperty.TenantId, Is.EqualTo(property.Tenant.UserId));
                Assert.That(outProperty.RentalFee, Is.EqualTo(property.RentalFee));
                Assert.That(outProperty.Address.Country, Is.EqualTo(property.Address.Country));
                Assert.That(outProperty.Address.City, Is.EqualTo(property.Address.City));
                Assert.That(outProperty.Address.ZipCode, Is.EqualTo(property.Address.ZipCode));
                Assert.That(outProperty.Address.Street, Is.EqualTo(property.Address.Street));
                Assert.That(outProperty.Address.StreetNumber, Is.EqualTo(property.Address.StreetNumber));
            });
        }

        [Test]
        public void CreatePropertyOutputDTO_TestCalculations_TalliesMatch()
        {
            Owner owner = new Owner()
            {
                UserId = Guid.NewGuid().ToString()
            };

            Property property = new Property()
            {
                Id = Guid.NewGuid(),
                Name = "ARealProperty",
                Owner = owner,
                Address = new Address()
                {
                    Country = "Corruptistan",
                    City = "Cesspit",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                },
                PurchaseDate = DateTime.Now,
                PurchasePrice = 50,
                Size = 35
            };

            Random random = new Random();
            decimal totalCosts = 0;
            decimal totalRevenues = 0;
            decimal totalValueIncreases = 0;
            decimal totalValueDecreases = 0;

            // Generate Costs
            for (int i = 0; i < random.Next(100); i++)
            {
                decimal value = (decimal)random.Next();
                totalCosts += value;

                property.Costs.Add(new Cost()
                {
                    Title = $"Cost {i}",
                    Date = DateTime.Now,
                    Value = value
                });
            }

            // Generate Revenues
            for (int i = 0; i < random.Next(100); i++)
            {
                decimal value = (decimal)random.Next();
                totalRevenues += value;

                property.Revenues.Add(new Revenue()
                {
                    Title = $"Revenue {i}",
                    Date = DateTime.Now,
                    Value = value
                });
            }

            // Generate ValueIncreases
            for (int i = 0; i < random.Next(100); i++)
            {
                decimal value = (decimal)random.Next();
                totalValueIncreases += value;

                property.ValueIncreases.Add(new ValueIncrease()
                {
                    Title = $"ValueIncrease {i}",
                    Date = DateTime.Now,
                    Value = value
                });
            }

            // Generate ValueDecreases
            for (int i = 0; i < random.Next(100); i++)
            {
                decimal value = (decimal)random.Next();
                totalValueDecreases += value;

                property.ValueDecreases.Add(new ValueDecrease()
                {
                    Title = $"ValueDecrease {i}",
                    Date = DateTime.Now,
                    Value = value
                });
            }

            IPropertyOutputDTO outProperty = _factory.CreatePropertyOutputDTO(property);

            Assert.Multiple(() =>
            {
                Assert.That(outProperty.Balance, Is.EqualTo(totalRevenues - totalCosts));
                Assert.That(outProperty.TotalCost, Is.EqualTo(totalCosts));
                Assert.That(outProperty.TotalRevenue, Is.EqualTo(totalRevenues));
                Assert.That(outProperty.CurrentValue, Is.EqualTo(totalValueIncreases - totalValueDecreases));
            });
        }
    }
}
