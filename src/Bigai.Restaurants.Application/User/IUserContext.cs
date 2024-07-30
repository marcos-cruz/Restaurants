namespace Bigai.Restaurants.Application.User;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}