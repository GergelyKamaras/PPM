using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIModelLibrary.Properties;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMAPIServiceLayer.OutputDTOFactory;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace PPMAPI.Controllers
{
    [Authorize]
    [Route("api/rentalproperties")]
    public class RentalPropertyController : ControllerBase
    {
        private readonly IPropertyFactory _propertyFactory;
        private readonly IPropertyOutputDTOFactory _propertyOutputDtoFactory;
        private readonly IRentalPropertiesQueries _rentalPropertiesQueries;

        public RentalPropertyController(IPropertyFactory propertyFactory, IPropertyOutputDTOFactory propertyOutputDtoFactory,
            IRentalPropertiesQueries rentalPropertiesQueries)
        {
            _propertyFactory = propertyFactory;
            _propertyOutputDtoFactory = propertyOutputDtoFactory;
            _rentalPropertiesQueries = rentalPropertiesQueries;
        }

        [HttpGet]
        [Route("{id}")]
        public IResult GetRentalPropertyById(string id)
        {
            RentalProperty property = _rentalPropertiesQueries.GetRentalPropertyById(id);
            IPropertyOutputDTO outProperty = _propertyOutputDtoFactory.CreatePropertyOutputDTO(property);

            return Results.Ok(outProperty);
        }

        [HttpGet]
        [Route("owners/{id}")]
        public IResult GetRentalPropertiesByOwner(string id)
        {
            List<RentalProperty> properties = _rentalPropertiesQueries.GetRentalPropertiesByOwnerId(id);

            List<IPropertyOutputDTO> outList = new List<IPropertyOutputDTO>();
            properties.ForEach(p => outList.Add(_propertyOutputDtoFactory.CreatePropertyOutputDTO(p)));

            return Results.Ok(outList);
        }

        [HttpPut]
        public IResult UpdateRentalProperty(RentalPropertyInputDTO propertyDTO)
        {
            RentalProperty property = _propertyFactory.CreateRentalProperty(propertyDTO);
            _rentalPropertiesQueries.UpdateRentalProperty(property);

            return Results.Ok();
        }

        [HttpDelete]
        public IResult DeleteRentalProperty(string id)
        {
            _rentalPropertiesQueries.DeleteRentalProperty(id);;
            return Results.Ok();
        }
    }
}
