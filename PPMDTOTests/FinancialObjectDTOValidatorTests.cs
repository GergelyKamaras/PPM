using PPMAPIServiceLayer.Validation;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPIServiceLayerTests
{
    internal class FinancialObjectDTOValidatorTests
    {
        private IFinancialInputDTOValidator _validator;
        
        [SetUp]
        public void Setup()
        {
            _validator = new FinancialInputDTOValidator();
        }

        [Test]
        public void Validate_MissingTitle_ReturnsFalse()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };
            Assert.That(_validator.Validate(input), Is.False);
        }

        [Test]
        public void Validate_MissingValue_ReturnsFalse()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Date = DateTime.Now,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.That(_validator.Validate(input), Is.False);
        }

        [Test]
        public void Validate_ZeroValue_ReturnsFalse()
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

            Assert.That(_validator.Validate(input), Is.False);
        }

        [Test]
        public void Validate_NegativeValue_ReturnsFalse()
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

            Assert.That(_validator.Validate(input), Is.False);
        }

        [Test]
        public void Validate_MissingDate_ReturnsFalse()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                IsRental = true,
                PropertyId = "Id"
            };

            Assert.That(_validator.Validate(input), Is.False);
        }

        [Test]
        public void Validate_MissingPropertyId_ReturnsFalse()
        {
            FinancialInputDTO input = new FinancialInputDTO()
            {
                Title = "ValidCost",
                Description = "A very valid cost",
                Value = 50,
                Date = DateTime.Now,
                IsRental = true,
            };

            Assert.That(_validator.Validate(input), Is.False);
        }
    }
}
