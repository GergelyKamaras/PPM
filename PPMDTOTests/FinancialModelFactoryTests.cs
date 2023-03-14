using PPMAPIServiceLayer.InputDTOConverter;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.Transactions;

namespace PPMDTOTests
{
    public class Tests
    {
        private FinancialModelFactory _factory;
        
        [SetUp]
        public void Setup()
        {
            _factory = new FinancialModelFactory();
        }

        [Test]
        public void CreateCost_ValidPropertyInput_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                IsRental = false,
                PropertyId = "Id"
            };

            Cost cost = _factory.CreateCost(input);

            Assert.Multiple(() =>
            {
                Assert.That(cost.Date, Is.EqualTo(input.Date));
                Assert.That(cost.Description, Is.EqualTo(input.Description));
                Assert.That(cost.Value, Is.EqualTo(input.Value));
                Assert.That(cost.Value, Is.EqualTo(input.Value));
                Assert.That(cost.Title, Is.EqualTo(input.Title));
                Assert.That(cost.RentalProperty, Is.Null);
            });
        }

        [Test]
        public void CreateCost_ValidRentalPropertyInput_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };

            Cost cost = _factory.CreateCost(input);

            Assert.Multiple(() =>
            {
                Assert.That(cost.Date, Is.EqualTo(input.Date));
                Assert.That(cost.Description, Is.EqualTo(input.Description));
                Assert.That(cost.Value, Is.EqualTo(input.Value));
                Assert.That(cost.Value, Is.EqualTo(input.Value));
                Assert.That(cost.Title, Is.EqualTo(input.Title));
                Assert.That(cost.Property, Is.Null);
            });
        }

        [Test]
        public void CreateCost_MissingTitle_ThrowsError()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateCost(input));
        }

        [Test]
        public void CreateCost_MissingValue_ThrowsError()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateCost(input));
        }

        [Test]
        public void CreateCost_ZeroValue_ThrowsError()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 0,
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateCost(input));
        }

        [Test]
        public void CreateCost_NegativeValue_ThrowsError()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = -1,
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateCost(input));
        }

        [Test]
        public void CreateCost_MissingDate_ThrowsError()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateCost(input));
        }

        [Test]
        public void CreateCost_MissingPropertyId_ThrowsError()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                IsRental = true,
            };

            Assert.Throws<ArgumentException>(() => _factory.CreateCost(input));
        }
    }
}