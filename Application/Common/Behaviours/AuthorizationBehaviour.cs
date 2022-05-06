using System.Reflection;
using Application.Common.Contracts;
using Application.Common.Interfaces;
using Application.Common.Security;
using Application.Exceptions;
using MediatR;


namespace Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUser _currentUserService;
        private readonly IIdentityService _identityService;

        public AuthorizationBehaviour(
            ICurrentUser currentUserService,
            IIdentityService identityService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Policy-based authorization
                var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                    {
                        var authorized = await _identityService.AuthorizeAsync(_currentUserService.UserId, policy);

                        if (!authorized.Succeeded)
                        {
                            throw new ForbiddenAccessException("Access denied.");
                        }
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
