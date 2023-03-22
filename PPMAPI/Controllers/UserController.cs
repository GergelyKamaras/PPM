using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPMAPIDataAccess.DbTableQueries.OwnersQueries;
using PPMAPIDataAccess.DbTableQueries.TenantsQueries;
using PPMAPIModelLibrary.Users;

namespace PPMAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IOwnersQueries _ownersQueries;
        private readonly ITenantsQueries _tenantsQueries;
        public UserController(IOwnersQueries ownersQueries, ITenantsQueries tenantsQueries)
        {
            _ownersQueries = ownersQueries;
            _tenantsQueries = tenantsQueries;
        }
        
        [HttpPost]
        public IResult AddUser(string userId, string role)
        {
            
            switch (role)
            {
                case ("Owner"):
                    if (_ownersQueries.GetOwnerById(userId) == null)
                    {
                        _ownersQueries.AddOwner(new Owner(){ UserId = userId});
                    }
                    break;
                case ("Tenant"):
                    if (_tenantsQueries.GetTenantById(userId) == null)
                    {
                        _tenantsQueries.AddTenant(new Tenant(){ UserId = userId});
                    }
                    break;
                default:
                    return Results.Problem("No such user role exists!");
            }
            return Results.Ok();
        }

        [HttpDelete]
        public IResult DeleteUser(string userId)
        {
            if (_ownersQueries.GetOwnerById(userId) != null)
            {
                _ownersQueries.DeleteOwner(userId);
                return Results.Ok();
            }

            if (_tenantsQueries.GetTenantById(userId) != null)
            {
                _tenantsQueries.DeleteTenant(userId);
                return Results.Ok();
            }
            return Results.Problem("No such user exists with the given Id!");
        }
    }
}
