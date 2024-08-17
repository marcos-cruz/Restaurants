using Bigai.Restaurants.Application.Users;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;
using Bigai.Restaurants.Infrastructure.Authorization.Requirements;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using Moq;

namespace Bigai.Restaurants.Infrastructure.Tests.Authorization.Requirements;

public class CreateMultipleRestaurantsRequirementTests
{
    [Fact]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateMultipleRestaurantsRequirementHandler>>();
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.UserId,
            },
            new()
            {
                OwnerId = currentUser.UserId,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreateMultipleRestaurantsRequirement(2);
        var handler = new CreateMultipleRestaurantsRequirementHandler(loggerMock.Object,
                                                                      userContextMock.Object,
                                                                      restaurantsRepositoryMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // act
        await handler.HandleAsync(context);

        // assert
        context.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldFail()
    {
        // arrange
        var loggerMock = new Mock<ILogger<CreateMultipleRestaurantsRequirementHandler>>();
        var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

        var restaurants = new List<Restaurant>()
        {
            new()
            {
                OwnerId = currentUser.UserId,
            },
            new()
            {
                OwnerId = "2",
            }
        };

        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantsRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(restaurants);

        var requirement = new CreateMultipleRestaurantsRequirement(2);
        var handler = new CreateMultipleRestaurantsRequirementHandler(loggerMock.Object,
                                                                      userContextMock.Object,
                                                                      restaurantsRepositoryMock.Object);
        var context = new AuthorizationHandlerContext([requirement], null, null);

        // act
        await handler.HandleAsync(context);

        // assert
        context.HasSucceeded.Should().BeFalse();
        context.HasFailed.Should().BeTrue();
    }
}