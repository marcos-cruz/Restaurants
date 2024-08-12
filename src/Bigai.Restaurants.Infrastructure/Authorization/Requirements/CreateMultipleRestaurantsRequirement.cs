using Microsoft.AspNetCore.Authorization;

namespace Bigai.Restaurants.Infrastructure.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirement : IAuthorizationRequirement
{
    public int MinimumRestaurantsCreated { get; }

    public CreateMultipleRestaurantsRequirement(int minimumRestaurantsCreated)
    {
        MinimumRestaurantsCreated = minimumRestaurantsCreated;
    }

}