using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.Users;
using PPMAPIModelLibrary.UtilityModels;
using PPMAPIServiceLayer.Validation;

namespace PPMAPIServiceLayerTests.Validation
{
    internal class PropertyValidatorTests
    {
        IPropertyValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new PropertyValidator();
        }

        [Test]
        public void Validate_MissingCountry_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingCity_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingStreet_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingStreetNumber_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingZipCode_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingId_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingName_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingSize_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_InvalidSize_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingPurchasePrice_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_InvalidPurchasePrice_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_MissingOwner_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

        [Test]
        public void Validate_InvalidRentalFee_ReturnsFalse()
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

            Assert.That(_validator.Validate(property), Is.False);
        }

    }
}
