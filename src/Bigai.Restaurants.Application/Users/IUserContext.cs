namespace Bigai.Restaurants.Application.Users;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}