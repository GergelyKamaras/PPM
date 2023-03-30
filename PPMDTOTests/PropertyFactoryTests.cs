using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMAPIModelLibrary.Properties;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPIServiceLayerTests
{
    internal class PropertyFactoryTests
    {
        private PropertyFactory _factory;
        
        [SetUp]
        public void Setup()
        {
            _factory = new PropertyFactory();
        }

        [Test]
        public void CreateProperty_ValidInput_PropertiesMatch()
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
                    StreetNumber = 1,
                    AdditionalInfo = "Nada"
                }
            };

            Property property = _factory.CreateProperty(propertyDTO);

            Assert.Multiple(() =>
            {
                Assert.That(property.Name, Is.EqualTo(propertyDTO.Name));
                Assert.That(property.PurchasePrice, Is.EqualTo(propertyDTO.PurchasePrice));
                Assert.That(property.PurchaseDate, Is.EqualTo(propertyDTO.PurchaseDate));
                Assert.That(property.Size, Is.EqualTo(propertyDTO.Size));
                Assert.That(property.Address.Country, Is.EqualTo(propertyDTO.Address.Country));
                Assert.That(property.Address.City, Is.EqualTo(propertyDTO.Address.City));
                Assert.That(property.Address.ZipCode, Is.EqualTo(propertyDTO.Address.ZipCode));
                Assert.That(property.Address.Street, Is.EqualTo(propertyDTO.Address.Street));
                Assert.That(property.Address.StreetNumber, Is.EqualTo(propertyDTO.Address.StreetNumber));
                Assert.That(property.Address.AdditionalInfo, Is.EqualTo(propertyDTO.Address.AdditionalInfo));
                Assert.That(property, Has.Property("Id"));
            });
        }

        [Test]
        public void CreateRentalProperty_ValidInput_PropertiesMatch()
        {
            PropertyInputDTO propertyDTO = new PropertyInputDTO()
            {
                Name = "RealProperty",
                OwnerId = "Id1",
                TenantId = "Id2",
                RentalFee = 50,
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

            RentalProperty property = _factory.CreateRentalProperty(propertyDTO);

            Assert.Multiple(() =>
            {
                Assert.That(property.Name, Is.EqualTo(propertyDTO.Name));
                Assert.That(property.PurchasePrice, Is.EqualTo(propertyDTO.PurchasePrice));
                Assert.That(property.PurchaseDate, Is.EqualTo(propertyDTO.PurchaseDate));
                Assert.That(property.Size, Is.EqualTo(propertyDTO.Size));
                Assert.That(property.RentalFee, Is.EqualTo(propertyDTO.RentalFee));
                Assert.That(property.Address.Country, Is.EqualTo(propertyDTO.Address.Country));
                Assert.That(property.Address.City, Is.EqualTo(propertyDTO.Address.City));
                Assert.That(property.Address.ZipCode, Is.EqualTo(propertyDTO.Address.ZipCode));
                Assert.That(property.Address.Street, Is.EqualTo(propertyDTO.Address.Street));
                Assert.That(property.Address.StreetNumber, Is.EqualTo(propertyDTO.Address.StreetNumber));
                Assert.That(property.Address.AdditionalInfo, Is.EqualTo(propertyDTO.Address.AdditionalInfo));
                Assert.That(property, Has.Property("Id"));
            });
        }
    }
}
