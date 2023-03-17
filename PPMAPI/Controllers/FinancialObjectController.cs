using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPMAPIDataAccess.DbTableQueries.CostsQueries;
using PPMAPIDataAccess.DbTableQueries.RevenuesQueries;
using PPMAPIDataAccess.DbTableQueries.ValueDecreasesQueries;
using PPMAPIDataAccess.DbTableQueries.ValueIncreasesQueries;
using PPMAPIDTOModelLibrary.OutputDTOs.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMAPIServiceLayer.OutputDTOFactory;
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

        public FinancialObjectController(ICostsQueries costsQueries,
            IRevenuesQueries revenuesQueries, IValueIncreasesQueries valueIncreasesQueries,
            IValueDecreasesQueries valueDecreasesQueries, IFinancialObjectFactory financialObjectFactory,
            IFinancialObjectOutputDTOFactory financialObjectOutputDTOFactory)
        {
            _costsQueries = costsQueries;
            _revenuesQueries = revenuesQueries;
            _valueIncreasesQueries = valueIncreasesQueries;
            _valueDecreasesQueries = valueDecreasesQueries;
            _financialObjectFactory = financialObjectFactory;
            _financialObjectOutputDTOFactory = financialObjectOutputDTOFactory;
        }

        [HttpGet]
        [Route("{type}/{id}")]
        public IResult GetFinancialObjectById(string type, int id)
        {
            IFinancialObjectOutputDTO dto = null;
            switch (type)
            {
                case (FinancialObject.Cost):
                    dto = _financialObjectOutputDTOFactory.Create(_costsQueries.GetCostById(id));
                    break;
                case (FinancialObject.Revenue):
                    dto = _financialObjectOutputDTOFactory.Create(_revenuesQueries.GetRevenueById(id));
                    break;
                case (FinancialObject.ValueIncrease):
                    dto = _financialObjectOutputDTOFactory.Create(_valueIncreasesQueries.GetValueIncreaseById(id));
                    break;
                case (FinancialObject.ValueDecrease):
                    dto = _financialObjectOutputDTOFactory.Create(_valueDecreasesQueries.GetValueDecreaseById(id));
                    break;
            }

            if (dto == null)
            {
                return Results.Problem("Could not find financial object of given type and id!");
            }

            return Results.Ok(dto);
        }

        [HttpPut]
        public IResult UpdateFinancialObjectById([FromForm] FinancialInputDTO input)
        {
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
            IFinancialObject financialObject = _financialObjectFactory.CreateFinancialObject(input);

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
