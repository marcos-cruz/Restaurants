using Bigai.Restaurants.Application.Users;
using Bigai.Restaurants.Domain.Constants;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Enums;
using Bigai.Restaurants.Domain.Services;

using Microsoft.Extensions.Logging;

namespace Bigai.Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService : IRestaurantAuthorizationService
{
    private readonly ILogger<RestaurantAuthorizationService> _logger;
    private readonly IUserContext _userContext;

    public RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
                                          IUserContext userContext)
    {
        _logger = logger;
        _userContext = userContext;
    }

    public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
    {
        var user = _userContext.GetCurrentUser();

        _logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for {Restaurant}",
                               user.Email,
                               resourceOperation,
                               restaurant.Name);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            _logger.LogInformation("Create/read operation - successful authrorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            _logger.LogInformation("Admim user, delete operation - succcessful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update) && user.UserId == restaurant.OwnerId)
        {
            _logger.LogInformation("Restaurant owner - succcessful authorization");
            return true;
        }

        return false;
    }
}