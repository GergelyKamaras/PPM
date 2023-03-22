using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.Properties;
using PPMAPIServiceLayer.Validation;

namespace PPMAPIServiceLayerTests.Validation
{
    internal class FinancialObjectValidatorTests
    {
        private IFinancialObjectValidator _validator;
        [SetUp]
        public void SetUp()
        {
            _validator = new FinancialObjectValidator();
        }

        [Test]
        public void Validate_MissingId_ReturnsFalse()
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
            Assert.That(_validator.Validate(cost), Is.False);
        }

        [Test]
        public void Validate_MissingTitle_ReturnsFalse()
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

            Assert.That(_validator.Validate(cost), Is.False);
        }

        [Test]
        public void Validate_MissingValue_ReturnsFalse()
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

            Assert.That(_validator.Validate(cost), Is.False);
        }

        [Test]
        public void Validate_InvalidValue_ReturnsFalse()
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

            Assert.That(_validator.Validate(cost), Is.False);
        }

        [Test]
        public void Validate_MissingDate_ReturnsFalse()
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

            Assert.That(_validator.Validate(cost), Is.False);
        }

        [Test]
        public void Validate_InvalidDate_ReturnsFalse()
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

            Assert.That(_validator.Validate(cost), Is.False);
        }
    }
}
