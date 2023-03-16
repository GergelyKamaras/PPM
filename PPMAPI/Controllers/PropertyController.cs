using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPMAPIDataAccess.DbTableQueries.CostsQueries;
using PPMAPIDataAccess.DbTableQueries.PropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.ValueIncreasesQueries;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMAPIServiceLayer.OutputDTOFactory;
using PPMAPIModelLibrary.Properties;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.FinancialObjects.ValueModifiers;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPI.Controllers
{
    [Authorize]
    [Route("api/properties")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyFactory _propertyFactory ;
        private readonly IPropertyOutputDTOFactory _propertyOutputDtoFactory;
        private readonly ICostsQueries _costsQueries;
        private readonly IPropertiesQueries _propertiesQueries;
        private readonly IRentalPropertiesQueries _rentalPropertiesQueries;
        private readonly IValueIncreasesQueries _valueIncreasesQueries;

        public PropertyController(IPropertyFactory propertyFactory, IPropertyOutputDTOFactory propertyOutputDtoFactory, 
            ICostsQueries costsQueries, IPropertiesQueries propertiesQueries, IRentalPropertiesQueries rentalPropertiesQueries, 
            IValueIncreasesQueries valueIncreasesQueries)
        {
            _propertyFactory = propertyFactory;
            _propertyOutputDtoFactory = propertyOutputDtoFactory;
            _costsQueries = costsQueries;
            _propertiesQueries = propertiesQueries;
            _rentalPropertiesQueries = rentalPropertiesQueries;
            _valueIncreasesQueries = valueIncreasesQueries;
        }

        [HttpGet]
        [Route("{id}")]
        public IResult GetPropertyById(string id)
        {
            Property property = _propertiesQueries.GetPropertyById(id);
            IPropertyOutputDTO outProperty = _propertyOutputDtoFactory.CreatePropertyOutputDTO(property);

            return Results.Ok(outProperty);
        }
        
        [HttpGet]
        [Route("owners/{id}")]
        public IResult GetPropertiesByOwner(string id)
        {
            List<Property> properties = _propertiesQueries.GetPropertiesByOwnerId(id);

            List<IPropertyOutputDTO> outList = new List<IPropertyOutputDTO>();
            properties.ForEach(p => outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(p)));

            return Results.Ok(outList);
        }

        [HttpPost]
        public IResult AddProperty([FromForm] IPropertyInputDTO protoProperty)
        {
            RentalProperty rentalProperty = null;
            Property property = null;

            if (protoProperty.IsRental)
            {
                rentalProperty = _propertyFactory.CreateRentalProperty((RentalPropertyInputDTO)protoProperty);
                _rentalPropertiesQueries.AddRentalProperty(rentalProperty);
            }
            else
            {
                property = _propertyFactory.CreateProperty((PropertyInputDTO)protoProperty);
                _propertiesQueries.AddProperty(property);
            }

            Cost initialCost = new Cost()
            {
                Title = "Purchase Price",
                Date = protoProperty.PurchaseDate,
                Value = protoProperty.PurchasePrice,
                Property = property,
                RentalProperty = rentalProperty
            };

            ValueIncrease initialValue = new ValueIncrease()
            {
                Title = "Purchase Value",
                Date = protoProperty.PurchaseDate,
                Value = protoProperty.PurchasePrice,
                Property = property,
                RentalProperty = rentalProperty
            };

            _valueIncreasesQueries.AddValueIncrease(initialValue);
            _costsQueries.AddCost(initialCost);

            return Results.Ok();
        }

        [HttpPut]
        public IResult UpdateProperty(PropertyInputDTO propertyDTO)
        {
            Property property = _propertyFactory.CreateProperty(propertyDTO);
            _propertiesQueries.UpdateProperty(property);
            
            return Results.Ok();
        }

        [HttpDelete]
        public IResult DeleteProperty(string id)
        {
            _propertiesQueries.DeleteProperty(id);
            return Results.Ok();
        }
    }
}
