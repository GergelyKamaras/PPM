using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPMAPIDataAccess.DbTableQueries.CostsQueries;
using PPMAPIDataAccess.DbTableQueries.PropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.RevenuesQueries;
using PPMAPIDataAccess.DbTableQueries.ValueDecreasesQueries;
using PPMAPIDataAccess.DbTableQueries.ValueIncreasesQueries;
using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMAPIModelLibrary.Properties;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMAPIServiceLayer.OutputDTOFactory;
using PPMAPIServiceLayer.Validation;
using PPMDTOModelLibrary.InputDTOs.FinancialInput;

namespace PPMAPI.Controllers
{

    [Authorize]
    [Route("api/financialobjects")]
    public class FinancialObjectController : ControllerBase
    {
        private readonly ICostsQueries _costsQueries;
        private readonly IRevenuesQueries _revenuesQueries;
        private readonly IValueIncreasesQueries _valueIncreasesQueries;
        private readonly IValueDecreasesQueries _valueDecreasesQueries;
        private readonly IFinancialObjectFactory _financialObjectFactory;
        private readonly IFinancialObjectOutputDTOFactory _financialObjectOutputDTOFactory;
        private readonly IFinancialInputDTOValidator _financialInputDTOValidator;
        private readonly IFinancialObjectValidator _financialObjectValidator;
        private readonly IPropertiesQueries _propertyQueries;
        private readonly IRentalPropertiesQueries _rentalPropertiesQueries;

        public FinancialObjectController(ICostsQueries costsQueries,
            IRevenuesQueries revenuesQueries, IValueIncreasesQueries valueIncreasesQueries,
            IValueDecreasesQueries valueDecreasesQueries, IFinancialObjectFactory financialObjectFactory,
            IFinancialObjectOutputDTOFactory financialObjectOutputDTOFactory, 
            IFinancialInputDTOValidator financialInputDTOValidator, IFinancialObjectValidator financialObjectValidator,
            IPropertiesQueries propertyQueries, IRentalPropertiesQueries rentalPropertiesQueries)
        {
            _costsQueries = costsQueries;
            _revenuesQueries = revenuesQueries;
            _valueIncreasesQueries = valueIncreasesQueries;
            _valueDecreasesQueries = valueDecreasesQueries;
            _financialObjectFactory = financialObjectFactory;
            _financialObjectOutputDTOFactory = financialObjectOutputDTOFactory;
            _financialInputDTOValidator = financialInputDTOValidator;
            _financialObjectValidator = financialObjectValidator;
            _propertyQueries = propertyQueries;
            _rentalPropertiesQueries = rentalPropertiesQueries;
        }

        [HttpGet]
        [Route("{type}/{id}")]
        public IResult GetFinancialObjectById(string type, int id)
        {
            IFinancialObject finObject = null;
            switch (type)
            {
                case (FinancialObject.Cost):
                    finObject = _costsQueries.GetCostById(id);
                    break;
                case (FinancialObject.Revenue):
                    finObject = _revenuesQueries.GetRevenueById(id);
                    break;
                case (FinancialObject.ValueIncrease):
                    finObject = _valueIncreasesQueries.GetValueIncreaseById(id);
                    break;
                case (FinancialObject.ValueDecrease):
                    finObject = _valueDecreasesQueries.GetValueDecreaseById(id);
                    break;
            }

            if (finObject == null)
            {
                return Results.Problem("Could not find financial object of given type and id!");
            }

            if (!_financialObjectValidator.Validate(finObject))
            {
                return Results.Problem("Invalid financial object model!");
            }

            IFinancialObjectOutputDTO dto = _financialObjectOutputDTOFactory.Create(finObject);

            return Results.Ok(dto);
        }

        [HttpPut]
        public IResult UpdateFinancialObject([FromForm] FinancialInputDTO input)
        {
            if (!_financialInputDTOValidator.Validate(input))
            {
                return Results.Problem("Error in input DTO!");
            }

            IFinancialObject financialObject = _financialObjectFactory.CreateFinancialObject(input);
            switch (input.FinancialObjectType)
            {
                case (FinancialObject.Cost):
                    _costsQueries.UpdateCost((Cost)financialObject);
                    break;
                case (FinancialObject.Revenue):
                    _revenuesQueries.UpdateRevenue((Revenue)financialObject);
                    break;
                case (FinancialObject.ValueIncrease):
                    _valueIncreasesQueries.UpdateValueIncrease((ValueIncrease)financialObject);
                    break;
                case (FinancialObject.ValueDecrease):
                    _valueDecreasesQueries.UpdateValueDecrease((ValueDecrease)financialObject);
                    break;
            }

            return Results.Ok();
        }

        [HttpDelete]
        [Route("{type}/{id}")]
        public IResult DeleteFinancialObjectById(string type, int id)
        {
            switch (type)
            {
                case (FinancialObject.Cost):
                    _costsQueries.DeleteCost(id);
                    break;
                case (FinancialObject.Revenue):
                    _revenuesQueries.DeleteRevenue(id);
                    break;
                case (FinancialObject.ValueIncrease):
                    _valueIncreasesQueries.DeleteValueIncrease(id);
                    break;
                case (FinancialObject.ValueDecrease):
                    _valueDecreasesQueries.DeleteValueDecrease(id);
                    break;
            }

            return Results.Ok();
        }

        [HttpPost]
        public IResult AddFinancialObject([FromForm] FinancialInputDTO input)
        {
            if (!_financialInputDTOValidator.Validate(input))
            {
                return Results.Problem("Error in input DTO!");
            }

            RentalProperty rentalProperty = _rentalPropertiesQueries.GetRentalPropertyById(input.PropertyId);
            Property property = _propertyQueries.GetPropertyById(input.PropertyId);

            IFinancialObject financialObject = _financialObjectFactory.CreateFinancialObject(input);
            financialObject.Property = property;
            financialObject.RentalProperty = rentalProperty;

            switch (input.FinancialObjectType)
            {
                case (FinancialObject.Cost):
                    _costsQueries.AddCost((Cost)financialObject);
                    break;
                case (FinancialObject.Revenue):
                    _revenuesQueries.AddRevenue((Revenue)financialObject);
                    break;
                case (FinancialObject.ValueIncrease):
                    _valueIncreasesQueries.AddValueIncrease((ValueIncrease)financialObject);
                    break;
                case (FinancialObject.ValueDecrease):
                    _valueDecreasesQueries.AddValueDecrease((ValueDecrease)financialObject);
                    break;
            }

            return Results.Ok();
        }
    }
}
