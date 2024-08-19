
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Bigai.Restaurants.Api.Tests;

public class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claimsPincipal = new ClaimsPrincipal();

        claimsPincipal.AddIdentity(new ClaimsIdentity(
            new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "Admin")
            })
        );

        AuthenticationTicket ticket = new AuthenticationTicket(claimsPincipal, "Test");
        AuthenticateResult result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
        var result = PolicyAuthorizationResult.Success();

        return Task.FromResult(result);
    }
}