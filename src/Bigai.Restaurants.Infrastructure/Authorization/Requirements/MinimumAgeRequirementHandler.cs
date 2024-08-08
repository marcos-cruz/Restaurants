using Bigai.Restaurants.Application.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    private readonly ILogger<MinimumAgeRequirementHandler> _logger;
    private readonly IUserContext _userContext;

    public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
                                        IUserContext userContext)
    {
        _logger = logger;
        _userContext = userContext;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var currentUser = _userContext.GetCurrentUser();

        _logger.LogInformation("user: {Email}, date of birth {DateOfBirth}", currentUser.Email, currentUser.DateOfBirth);

        if (currentUser.DateOfBirth is null)
        {
            _logger.LogInformation("User date of birth is null");
            context.Fail();

            return Task.CompletedTask;
        }

        var currentAge = DateOnly.FromDateTime(DateTime.Today);
        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= currentAge)
        {
            _logger.LogInformation("Authorization suceeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
