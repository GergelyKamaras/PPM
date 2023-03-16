using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.Users;
using PPMAPIModelLibrary.UtilityModels;
using PPMAPIServiceLayer.OutputDTOFactory;
using System.Drawing;

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
        public void CreatePropertyOutputDTO_MissingCountry_ThrowsError()
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

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingCity_ThrowsError()
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

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingStreet_ThrowsError()
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
                    StreetNumber = 1,
                },
                PurchaseDate = DateTime.Now,
                PurchasePrice = 50,
                Size = 35,
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingStreetNumber_ThrowsError()
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
                },
                PurchaseDate = DateTime.Now,
                PurchasePrice = 50,
                Size = 35,
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingZipCode_ThrowsError()
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
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                },
                PurchaseDate = DateTime.Now,
                PurchasePrice = 50,
                Size = 35,
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingId_ThrowsError()
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

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingName_ThrowsError()
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

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingSize_ThrowsError()
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
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_InvalidSize_ThrowsError()
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
                Size = -1,
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingPurchasePrice_ThrowsError()
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
                Size = 35,
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_InvalidPurchasePrice_ThrowsError()
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
                PurchasePrice = -1,
                Size = 35,
                Tenant = tenant,
                RentalFee = 60
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_MissingOwner_ThrowsError()
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

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }

        [Test]
        public void CreatePropertyOutputDTO_InvalidRentalFee_ThrowsError()
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
                RentalFee = -1
            };

            Assert.Throws<ArgumentException>(() => _factory.CreatePropertyOutputDTO(property));
        }
    }
}
