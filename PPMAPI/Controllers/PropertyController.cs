using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
using PPMAPIModelLibrary.Users;
using PPMAPIServiceLayer.Validation;
using PPMDTOModelLibrary.InputDTOs.Properties;
using PPMAPIDataAccess.DbTableQueries.OwnersQueries;

namespace PPMAPI.Controllers
{
    [Authorize]
    [Route("api/properties")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyFactory _propertyFactory;
        private readonly IPropertyOutputDTOFactory _propertyOutputDtoFactory;
        private readonly ICostsQueries _costsQueries;
        private readonly IPropertiesQueries _propertiesQueries;
        private readonly IRentalPropertiesQueries _rentalPropertiesQueries;
        private readonly IValueIncreasesQueries _valueIncreasesQueries;
        private readonly IPropertyInputDTOValidator _propertyInputDTOValidator;
        private readonly IPropertyValidator _propertyValidator;
        private readonly IOwnersQueries _ownersQueries;

        private const string RentalPropertyType = "rental";
        private const string BasePropertyType = "property";

        public PropertyController(IPropertyFactory propertyFactory, IPropertyOutputDTOFactory propertyOutputDtoFactory,
            ICostsQueries costsQueries, IPropertiesQueries propertiesQueries, IRentalPropertiesQueries rentalPropertiesQueries,
            IValueIncreasesQueries valueIncreasesQueries, IPropertyInputDTOValidator propertyInputDTOValidator,
            IPropertyValidator propertyValidator, IOwnersQueries ownersQueries)
        {
            _propertyFactory = propertyFactory;
            _propertyOutputDtoFactory = propertyOutputDtoFactory;
            _costsQueries = costsQueries;
            _propertiesQueries = propertiesQueries;
            _rentalPropertiesQueries = rentalPropertiesQueries;
            _valueIncreasesQueries = valueIncreasesQueries;
            _propertyInputDTOValidator = propertyInputDTOValidator;
            _propertyValidator = propertyValidator;
            _ownersQueries = ownersQueries;
        }

        [HttpGet]
        [Route("{type}/{id}")]
        public IResult GetPropertyById(string type, string id)
        {
            switch (type)
            {
                case (RentalPropertyType):
                    RentalProperty rentalProperty = _rentalPropertiesQueries.GetRentalPropertyById(id);
                    if (_propertyValidator.Validate(rentalProperty))
                    {
                        IPropertyOutputDTO outRentalProperty = _propertyOutputDtoFactory.CreatePropertyOutputDTO(rentalProperty);

                        return Results.Ok(outRentalProperty);
                    }

                    return Results.Problem("Error in property model!");

                case (BasePropertyType):
                    Property property = _propertiesQueries.GetPropertyById(id);
                    if (_propertyValidator.Validate(property))
                    {
                        IPropertyOutputDTO outProperty = _propertyOutputDtoFactory.CreatePropertyOutputDTO(property);

                        return Results.Ok(outProperty);
                    }

                    return Results.Problem("Error in property model!");
                
                default:
                    return Results.Problem("Not a valid property type!");
            }
        }

        [HttpGet]
        [Route("{type}/owners/{id}")]
        public IResult GetPropertiesByTypeByOwner(string type, string id)
        {
            List<IPropertyOutputDTO> outList = new List<IPropertyOutputDTO>();
            switch (type)
            {
                case (RentalPropertyType):
                    List<RentalProperty> rentalProperties = _rentalPropertiesQueries.GetRentalPropertiesByOwnerId(id);

                    foreach (RentalProperty rentalProperty in rentalProperties)
                    {
                        if (!_propertyValidator.Validate(rentalProperty))
                        {
                            return Results.Problem("Error in property model!");
                        }

                        outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(rentalProperty));
                    }

                    return Results.Ok(outList);

                case (BasePropertyType):
                    List<Property> properties = _propertiesQueries.GetPropertiesByOwnerId(id);

                    foreach (Property property in properties)
                    {
                        if (!_propertyValidator.Validate(property))
                        {
                            return Results.Problem("Error in property model!");
                        }

                        outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(property));
                    }

                    return Results.Ok(outList);

                default:
                    return Results.Problem("Not a valid property type!");
            }
        }

        [HttpGet]
        [Route("owners/{id}")]
        public IResult GetAllPropertiesByOwner(string id)
        {
            List<IPropertyOutputDTO> outList = new List<IPropertyOutputDTO>();
            
            List<RentalProperty> rentalProperties = _rentalPropertiesQueries.GetRentalPropertiesByOwnerId(id);
            foreach (RentalProperty rentalProperty in rentalProperties)
            {
                if (!_propertyValidator.Validate(rentalProperty))
                {
                    return Results.Problem("Error in property model!");
                }

                outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(rentalProperty));
            }

            List<Property> properties = _propertiesQueries.GetPropertiesByOwnerId(id);
            foreach (Property property in properties)
            {
                if (!_propertyValidator.Validate(property))
                {
                    return Results.Problem("Error in property model!");
                }

                outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(property));
            }


            return Results.Ok(outList);
        }

        [HttpPost]
        public IResult AddProperty([FromBody] PropertyInputDTO protoProperty)
        {
            if (!_propertyInputDTOValidator.Validate(protoProperty))
            {
                return Results.Problem("Error in property input DTO!");
            }

            RentalProperty rentalProperty = null;
            Property property = null;
            Owner owner = _ownersQueries.GetOwnerById(protoProperty.OwnerId);

            if (protoProperty.IsRental)
            {
                rentalProperty = _propertyFactory.CreateRentalProperty(protoProperty);
                rentalProperty.Owner = owner;
                _rentalPropertiesQueries.AddRentalProperty(rentalProperty);
            }
            else
            {
                property = _propertyFactory.CreateProperty(protoProperty);
                property.Owner = owner;
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
            if (!_propertyInputDTOValidator.Validate(propertyDTO))
            {
                return Results.Problem("Error in property input DTO!");
            }

            if (propertyDTO.IsRental)
            {
                RentalProperty property = _propertyFactory.CreateRentalProperty(propertyDTO);
                _rentalPropertiesQueries.UpdateRentalProperty(property);
            }
            else
            {
                Property property = _propertyFactory.CreateProperty(propertyDTO);
                _propertiesQueries.UpdateProperty(property);
            }
            return Results.Ok();
        }

        [HttpDelete]
        [Route("{type}/{id}")]
        public IResult DeleteProperty(string type, string id)
        {
            switch (type)
            {
                case (RentalPropertyType):
                    _rentalPropertiesQueries.DeleteRentalProperty(id);
                    break;
                case (BasePropertyType):
                    _propertiesQueries.DeleteProperty(id);
                    break;
                default:
                    return Results.Problem("Not a valid property type!");
            }
            return Results.Ok();
        }
    }
}
