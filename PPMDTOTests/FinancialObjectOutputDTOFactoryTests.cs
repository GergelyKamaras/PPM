using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMAPIModelLibrary.Properties;
using PPMAPIServiceLayer.OutputDTOFactory;

namespace PPMAPIServiceLayerTests
{
    internal class FinancialObjectOutputDTOFactoryTests
    {
        private FinancialObjectOutputDTOFactory _factory;
        
        [SetUp]
        public void Setup()
        {
            _factory = new FinancialObjectOutputDTOFactory();
        }

        [Test]
        public void Create_ValidCostInput_ValidOutput()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "ValidExpense",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            IFinancialObjectOutputDTO output = _factory.Create(cost);

            Assert.Multiple(() =>
            {
                Assert.That(output.FinancialObjectType, Is.EqualTo("Cost"));
                Assert.That(output.Id, Is.EqualTo(cost.Id));
                Assert.That(output.Title, Is.EqualTo(cost.Title));
                Assert.That(output.Value, Is.EqualTo(cost.Value));
                Assert.That(output.Date, Is.EqualTo(cost.Date));
                Assert.That(output.Description, Is.EqualTo(cost.Description));
                Assert.That(output.PropertyId, Is.EqualTo(cost.Property.Id.ToString()));
            });
        }

        [Test]
        public void Create_ValidRevenueInput_ValidOutput()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "ValidRevenue",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            IFinancialObjectOutputDTO output = _factory.Create(revenue);

            Assert.Multiple(() =>
            {
                Assert.That(output.FinancialObjectType, Is.EqualTo("Revenue"));
                Assert.That(output.Id, Is.EqualTo(revenue.Id));
                Assert.That(output.Title, Is.EqualTo(revenue.Title));
                Assert.That(output.Value, Is.EqualTo(revenue.Value));
                Assert.That(output.Date, Is.EqualTo(revenue.Date));
                Assert.That(output.Description, Is.EqualTo(revenue.Description));
                Assert.That(output.PropertyId, Is.EqualTo(revenue.Property.Id.ToString()));
            });
        }

        [Test]
        public void Create_ValidValueIncreaseInput_ValidOutput()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "ValidRevenue",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            IFinancialObjectOutputDTO output = _factory.Create(valueIncrease);

            Assert.Multiple(() =>
            {
                Assert.That(output.FinancialObjectType, Is.EqualTo("ValueIncrease"));
                Assert.That(output.Id, Is.EqualTo(valueIncrease.Id));
                Assert.That(output.Title, Is.EqualTo(valueIncrease.Title));
                Assert.That(output.Value, Is.EqualTo(valueIncrease.Value));
                Assert.That(output.Date, Is.EqualTo(valueIncrease.Date));
                Assert.That(output.Description, Is.EqualTo(valueIncrease.Description));
                Assert.That(output.PropertyId, Is.EqualTo(valueIncrease.Property.Id.ToString()));
            });
        }

        [Test]
        public void Create_ValidValueDecreaseInput_ValidOutput()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "ValidRevenue",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            IFinancialObjectOutputDTO output = _factory.Create(valueDecrease);

            Assert.Multiple(() =>
            {
                Assert.That(output.FinancialObjectType, Is.EqualTo("ValueDecrease"));
                Assert.That(output.Id, Is.EqualTo(valueDecrease.Id));
                Assert.That(output.Title, Is.EqualTo(valueDecrease.Title));
                Assert.That(output.Value, Is.EqualTo(valueDecrease.Value));
                Assert.That(output.Date, Is.EqualTo(valueDecrease.Date));
                Assert.That(output.Description, Is.EqualTo(valueDecrease.Description));
                Assert.That(output.PropertyId, Is.EqualTo(valueDecrease.Property.Id.ToString()));
            });
        }

        [Test]
        public void Create_ValidInput_MapsPropertyTypesCorrectly()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid()
            };

            Cost costProperty = new Cost()
            {
                Id = 1,
                Title = "ValidExpense",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Cost costRentalProperty = new Cost()
            {
                Id = 2,
                Title = "ValidExpense2",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = null,
                RentalProperty = rentalProperty
            };

            IFinancialObjectOutputDTO propertyOutput = _factory.Create(costProperty);
            IFinancialObjectOutputDTO rentalPropertyOutput = _factory.Create(costRentalProperty);
            Assert.Multiple(() =>
            {
                Assert.That(propertyOutput.IsRental, Is.False);
                Assert.That(propertyOutput.PropertyId, Is.EqualTo(property.Id.ToString()));
                Assert.That(rentalPropertyOutput.IsRental, Is.True);
                Assert.That(rentalPropertyOutput.PropertyId, Is.EqualTo(rentalProperty.Id.ToString()));
            });
        }

        [Test]
        public void Create_MissingId_ThrowsError()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Title = "ValidExpense",
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Assert.Throws<ArgumentException>(() => _factory.Create(cost));
        }

        [Test]
        public void Create_MissingTitle_ThrowsError()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Id = 1,
                Value = 50,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Assert.Throws<ArgumentException>(() => _factory.Create(cost));
        }

        [Test]
        public void Create_MissingValue_ThrowsError()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "ValidExpense",
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Assert.Throws<ArgumentException>(() => _factory.Create(cost));
        }

        [Test]
        public void Create_InvalidValue_ThrowsError()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "ValidExpense",
                Value = -1,
                Date = DateTime.Now,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Assert.Throws<ArgumentException>(() => _factory.Create(cost));
        }

        [Test]
        public void Create_MissingDate_ThrowsError()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "ValidExpense",
                Value = 50,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Assert.Throws<ArgumentException>(() => _factory.Create(cost));
        }

        [Test]
        public void Create_InvalidDate_ThrowsError()
        {
            Property property = new Property()
            {
                Id = Guid.NewGuid(),
            };

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "ValidExpense",
                Value = 50,
                Date = DateTime.MinValue,
                Description = null,
                Property = property,
                RentalProperty = null
            };

            Assert.Throws<ArgumentException>(() => _factory.Create(cost));
        }
    }
}
