using Bigai.Restaurants.Application.Users;
using Bigai.Restaurants.Domain.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
{
    private readonly ILogger<CreateMultipleRestaurantsRequirementHandler> _logger;
    private readonly IUserContext _userContext;
    private readonly IRestaurantsRepository _restaurantsRepository;

    public CreateMultipleRestaurantsRequirementHandler(ILogger<CreateMultipleRestaurantsRequirementHandler> logger,
                                                       IUserContext userContext,
                                                       IRestaurantsRepository restaurantsRepository)
    {
        _logger = logger;
        _userContext = userContext;
        _restaurantsRepository = restaurantsRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
    {
        var currentUser = _userContext.GetCurrentUser();
        var restaurants = await _restaurantsRepository.GetAllAsync();
        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.UserId);

        if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            _logger.LogInformation("Authorization suceeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
