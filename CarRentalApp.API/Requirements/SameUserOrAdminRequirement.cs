using Microsoft.AspNetCore.Authorization;

namespace CarRentalApp.API.Requirements
{
    public class SameUserOrAdminRequirement : IAuthorizationRequirement { }
   
}
