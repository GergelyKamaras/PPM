using PPMAPIModelLibrary.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPIServiceLayerTests
{
    public class CreateFinancialObjectTests
    {
        private FinancialObjectFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new FinancialObjectFactory();
        }

        [Test]
        public void CreateFinancialObject_CreateRevenue_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidRevenue",
                Description = "A very valid transaction",
                Value = 50,
                Date = DateTime.Now,
                PropertyId = "Id",
                FinancialObjectType = "Revenue"
            };

            IFinancialObject revenue = _factory.CreateFinancialObject(input);

            Assert.Multiple(() =>
            {
                Assert.That(revenue.Date, Is.EqualTo(input.Date));
                Assert.That(revenue.Description, Is.EqualTo(input.Description));
                Assert.That(revenue.Value, Is.EqualTo(input.Value));
                Assert.That(revenue.Value, Is.EqualTo(input.Value));
                Assert.That(revenue.Title, Is.EqualTo(input.Title));
                Assert.That(revenue.RentalProperty, Is.Null);
                Assert.That(revenue.GetType(), Is.SameAs(typeof(Revenue)));
            });
        }

        [Test]
        public void CreateFinancialObject_CreateCost_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid transaction",
                Value = 50,
                Date = DateTime.Now,
                PropertyId = "Id",
                FinancialObjectType = "Cost"
            };

            IFinancialObject cost = _factory.CreateFinancialObject(input);

            Assert.Multiple(() =>
            {
                Assert.That(cost.Date, Is.EqualTo(input.Date));
                Assert.That(cost.Description, Is.EqualTo(input.Description));
                Assert.That(cost.Value, Is.EqualTo(input.Value));
                Assert.That(cost.Value, Is.EqualTo(input.Value));
                Assert.That(cost.Title, Is.EqualTo(input.Title));
                Assert.That(cost.RentalProperty, Is.Null);
                Assert.That(cost.GetType(), Is.SameAs(typeof(Cost)));
            });
        }

        [Test]
        public void CreateFinancialObject_CreateValueIncrease_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidValueIncrease",
                Description = "A very valid transaction",
                Value = 50,
                Date = DateTime.Now,
                PropertyId = "Id",
                FinancialObjectType = "ValueIncrease"
            };

            IFinancialObject valueIncrease = _factory.CreateFinancialObject(input);

            Assert.Multiple(() =>
            {
                Assert.That(valueIncrease.Date, Is.EqualTo(input.Date));
                Assert.That(valueIncrease.Description, Is.EqualTo(input.Description));
                Assert.That(valueIncrease.Value, Is.EqualTo(input.Value));
                Assert.That(valueIncrease.Value, Is.EqualTo(input.Value));
                Assert.That(valueIncrease.Title, Is.EqualTo(input.Title));
                Assert.That(valueIncrease.RentalProperty, Is.Null);
                Assert.That(valueIncrease.GetType(), Is.SameAs(typeof(ValueIncrease)));
            });
        }

        [Test]
        public void CreateFinancialObject_CreateValueDecrease_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidValueDecrease",
                Description = "A very valid transaction",
                Value = 50,
                Date = DateTime.Now,
                PropertyId = "Id",
                FinancialObjectType = "ValueDecrease"
            };

            IFinancialObject valueDecrease = _factory.CreateFinancialObject(input);

            Assert.Multiple(() =>
            {
                Assert.That(valueDecrease.Date, Is.EqualTo(input.Date));
                Assert.That(valueDecrease.Description, Is.EqualTo(input.Description));
                Assert.That(valueDecrease.Value, Is.EqualTo(input.Value));
                Assert.That(valueDecrease.Value, Is.EqualTo(input.Value));
                Assert.That(valueDecrease.Title, Is.EqualTo(input.Title));
                Assert.That(valueDecrease.RentalProperty, Is.Null);
                Assert.That(valueDecrease.GetType(), Is.SameAs(typeof(ValueDecrease)));
            });
        }

        [Test]
        public void CreateFinancialObject_ValidPropertyInput_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid transaction",
                Value = 50,
                Date = DateTime.Now,
                PropertyId = "Id",
                FinancialObjectType = "Cost"
            };

            IFinancialObject cost = _factory.CreateFinancialObject(input);

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
        public void CreateFinancialObject_ValidRentalPropertyInput_ValuesMatch()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                PropertyId = "Id",
                FinancialObjectType = "Cost"
            };

            IFinancialObject cost = _factory.CreateFinancialObject(input);

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
    }
}