using AutoMapper;

using Bigai.Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Bigai.Restaurants.Application.Users;
using Bigai.Restaurants.Domain.Entities;
using Bigai.Restaurants.Domain.Repositories;

using Castle.Core.Logging;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace Bigai.Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // arrange
        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var mapperMock = new Mock<IMapper>();
        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        var userContextMock = new Mock<IUserContext>();
        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new CreateRestaurantCommandHandler(restaurantRepositoryMock.Object,
                                                                loggerMock.Object,
                                                                mapperMock.Object,
                                                                userContextMock.Object);

        // act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be("owner-id");
        restaurantRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);
    }
}