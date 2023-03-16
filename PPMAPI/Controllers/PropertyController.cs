using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPMAPIDataAccess.DbTableQueries.AddressQueries;
using PPMAPIDataAccess.DbTableQueries.CostsQueries;
using PPMAPIDataAccess.DbTableQueries.OwnersQueries;
using PPMAPIDataAccess.DbTableQueries.PropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.RevenuesQueries;
using PPMAPIDataAccess.DbTableQueries.TenantsQueries;
using PPMAPIDataAccess.DbTableQueries.ValueDecreasesQueries;
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
        private readonly IFinancialObjectFactory _financialObjectFactory;
        private readonly IPropertyFactory _propertyFactory ;
        private readonly IFinancialObjectOutputDTOFactory _financialObjectOutputDtoFactory;
        private readonly IPropertyOutputDTOFactory _propertyOutputDtoFactory;
        private readonly IAddressesQueries _addressesQueries;
        private readonly ICostsQueries _costsQueries;
        private readonly IOwnersQueries _ownersQueries;
        private readonly IPropertiesQueries _propertiesQueries;
        private readonly IRentalPropertiesQueries _rentalPropertiesQueries;
        private readonly IRevenuesQueries _revenuesQueries;
        private readonly ITenantsQueries _tenantsQueries;
        private readonly IValueDecreasesQueries _valueDecreasesQueries;
        private readonly IValueIncreasesQueries _valueIncreasesQueries;

        public PropertyController(IFinancialObjectFactory financialObjectFactory, 
            IPropertyFactory propertyFactory, IFinancialObjectOutputDTOFactory financialObjectOutputDtoFactory,
            IPropertyOutputDTOFactory propertyOutputDtoFactory, IAddressesQueries addressesQueries,
            ICostsQueries costsQueries, IOwnersQueries ownersQueries, IPropertiesQueries propertiesQueries,
            IRentalPropertiesQueries rentalPropertiesQueries, IRevenuesQueries revenuesQueries, ITenantsQueries tenantsQueries,
            IValueDecreasesQueries valueDecreasesQueries, IValueIncreasesQueries valueIncreasesQueries)
        {
            _financialObjectFactory = financialObjectFactory;
            _propertyFactory = propertyFactory;
            _financialObjectOutputDtoFactory = financialObjectOutputDtoFactory;
            _propertyOutputDtoFactory = propertyOutputDtoFactory;
            _addressesQueries = addressesQueries;
            _costsQueries = costsQueries;
            _ownersQueries = ownersQueries;
            _propertiesQueries = propertiesQueries;
            _rentalPropertiesQueries = rentalPropertiesQueries;
            _revenuesQueries = revenuesQueries;
            _tenantsQueries = tenantsQueries;
            _valueDecreasesQueries = valueDecreasesQueries;
            _valueIncreasesQueries = valueIncreasesQueries;
        }

        [HttpGet]
        [Route("owners/{id}")]
        public IResult GetPropertiesByOwner(string id)
        {
            List<Property> properties = _propertiesQueries.GetPropertiesByOwnerId(id);
            List<RentalProperty> rentalProperties = _rentalPropertiesQueries.GetRentalPropertiesByOwnerId(id);

            List<IPropertyOutputDTO> outList = new List<IPropertyOutputDTO>();
            properties.ForEach(p => outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(p)));
            rentalProperties.ForEach(p => outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(p)));

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
    }
}
