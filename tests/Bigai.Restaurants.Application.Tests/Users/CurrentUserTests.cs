using Bigai.Restaurants.Application.Users;
using Bigai.Restaurants.Domain.Constants;

using FluentAssertions;

namespace Bigai.Restaurants.Application.Tests.Users;

public class CurrentUserTests
{
    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnTrue()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin);

        // Assert
        isInRole.Should().BeTrue();
    }

    [Fact]
    public void IsInRole_WithMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // Assert
        isInRole.Should().BeFalse();
    }

    [Fact]
    public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
    {
        // Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // Assert
        isInRole.Should().BeFalse();
    }
}