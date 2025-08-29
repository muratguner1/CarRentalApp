using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalApp.API.Requirements
{
    public class SameUserOrAdminHandler
        
        : AuthorizationHandler<SameUserOrAdminRequirement, int>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SameUserOrAdminRequirement requirement,
            int resourceUserId)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = context.User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleClaim == "Admin")
            {
                context.Succeed(requirement);
            }
            else if(userIdClaim != null && int.Parse(userIdClaim) == resourceUserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
