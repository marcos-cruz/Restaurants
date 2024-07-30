namespace Bigai.Restaurants.Application.User;

public record CurrentUser(string UserId, string Email, IEnumerable<string> Roles)
{
    public bool IsInROle(string role) => Roles.Contains(role);
}
