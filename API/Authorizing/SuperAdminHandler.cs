using Microsoft.AspNetCore.Authorization;

namespace API.Authorizing
{
   public class SuperAdminHandler
    : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRequirement requirement)
        {
            if (context.User.IsInRole("superadmin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}