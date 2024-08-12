using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Enums;

namespace Bigai.Restaurants.Domain.Services;

public interface IRestaurantAuthorizationService
{
    bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
}