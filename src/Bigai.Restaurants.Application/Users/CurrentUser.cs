namespace Bigai.Restaurants.Application.Users;

public record CurrentUser(string UserId,
                          string Email,
                          IEnumerable<string> Roles,
                          string? Nationality,
                          DateOnly? DateOfBirth)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
