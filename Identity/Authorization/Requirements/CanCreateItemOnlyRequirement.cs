using Microsoft.AspNetCore.Authorization;

namespace Identity.Authorization.Requirements
{
    public class CanCreateItemOnlyRequirement : IAuthorizationRequirement
    {
    }

    public class CanCreateItemOnlyRequirementHandler : AuthorizationHandler<CanCreateItemOnlyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanCreateItemOnlyRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Permission" && c.Value == "CanCreateItem"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
