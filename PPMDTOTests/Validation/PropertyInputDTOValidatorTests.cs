using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMAPIServiceLayer.Validation;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayerTests.Validation
{
    internal class PropertyInputDTOValidatorTests
    {
        private IPropertyInputDTOValidator _validator;
        [SetUp]
        public void Setup()
        {
            _validator = new PropertyInputDTOValidator();
        }

        [Test]
        public void ValidateRentalProperty_InValidRentalFee_ReturnsFalse()
        {
            RentalPropertyInputDTO propertyDTO = new RentalPropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                TenantId = "Id2",
                RentalFee = -1,
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                IsRental = true,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };
            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void ValidatePropertyData_MissingName_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void ValidatePropertyData_MissingSize_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void ValidatePropertyData_InvalidSize_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = -1,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void ValidatePropertyData_MissingPurchasePrice_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void ValidatePropertyData_InvalidPurchasePrice_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = -1,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };
            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void ValidatePropertyData_MissingOwnerId_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };
            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void CreateAddress_MissingCountry_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void CreateAddress_MissingCity_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void CreateAddress_MissingZipCode_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    Street = "HighwayToHell",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void CreateAddress_MissingStreet_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void CreateAddress_MissingStreetNumber_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }

        [Test]
        public void CreateAddress_InvalidStreetNumber_ReturnsFalse()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                PurchasePrice = 50,
                PurchaseDate = DateTime.Now,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Corruptistan",
                    City = "RudeAPest",
                    ZipCode = "666",
                    Street = "HighwayToHell",
                    StreetNumber = -1,
                    AdditionalInfo = "Nada"
                }
            };

            Assert.That(_validator.Validate(propertyDTO), Is.False);
        }
    }
}
